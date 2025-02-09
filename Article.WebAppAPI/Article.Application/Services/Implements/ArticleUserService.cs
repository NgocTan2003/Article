using Article.Application.Repositories.Interfaces;
using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Data.EF;
using Article.Data.Entity;
using Article.Dtos.UserDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Article.Application.Repositories.Implements;
using Article.Dtos.Categories;

namespace Article.Application.Services.Implements
{
    public class ArticleUserService : IArticleUserService
    {
        private readonly IArticleUserRepository _articleUserRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<ArticleAppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ArticleAppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly IAwsS3Service _awsS3Service;
        private readonly RoleManager<ArticleAppRole> _roleManager;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private IConfiguration _configuration;

        public ArticleUserService(IArticleUserRepository articleUserRepository, SignInManager<ArticleAppUser> signInManager, IActionContextAccessor actionContextAccessor,
            IMapper mapper, ITokenService tokenService, IAwsS3Service awsS3Service, IEmailService emailService, IUrlHelperFactory urlHelperFactory, IConfiguration configuration,
            UserManager<ArticleAppUser> userManager, IConfiguration config, ApplicationDbContext context, RoleManager<ArticleAppRole> roleManager)
        {
            _articleUserRepository = articleUserRepository;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _awsS3Service = awsS3Service;
            _mapper = mapper;
            _emailService = emailService;
            _userManager = userManager;
            _config = config;
            _context = context;
            _roleManager = roleManager;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _configuration = configuration;
        }

        public async Task<TokenResponse> Authentication(AuthenticationRequest request)
        {
            var result = new TokenResponse();
            var user = await _articleUserRepository.GetUserByUsername(request.UserName);

            if (user == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Message = "No user found";
                return result;
            }
            else if (await _userManager.IsLockedOutAsync(user))
            {
                result.StatusCode = StatusCodes.Status423Locked;
                result.Message = $"The account is locked {user.LockoutEnd}";
                return result;
            }
            else if (!user.EmailConfirmed)
            {
                result.StatusCode = StatusCodes.Status401Unauthorized;
                result.Message = "The account with emails have not been authenticated ";
                return result;
            }

            // account login bằng 2 bước 
            if (user.TwoFactorEnabled)
            {
                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
                    var tokenOTP = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    var message = _emailService.ChangeToMessageEmail(user.Email!, "OTP Confrimation", tokenOTP);
                    var resultSend = await _emailService.SendEmail(message);
                    if (resultSend.StatusCode == StatusCodes.Status200OK)
                    {
                        result.StatusCode = StatusCodes.Status422UnprocessableEntity;
                        result.Message = "Accounts must be login with OTP";
                        return result;
                    }
                    else
                    {
                        result.StatusCode = resultSend.StatusCode;
                        result.Message = resultSend.Message;
                        return result;
                    }
                }
                else
                {
                    await _userManager.AccessFailedAsync(user);
                    result.StatusCode = StatusCodes.Status401Unauthorized;
                    result.Message = "Invalid password";
                    return result;
                }
            }

            var SignIn = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (SignIn.Succeeded)
            {
                var roles = await _articleUserRepository.GetAllRoleByName(user.UserName);
                var claimUser = new ClaimUserLogin()
                {
                    Id = user.Id,
                    UserName = request.UserName,
                    Email = user.Email,
                    Roles = roles
                };
                var token = _tokenService.GenerateAccessToken(claimUser);
                var refeshToken = _tokenService.GenerateRefreshToken();
                var timeToken = int.TryParse(_config["JWT:TokenExpirationTime"], out int tokenExpirationTime);
                var timeRefeshToken = int.TryParse(_config["JWT:RefreshTokenExpirationTime"], out int refeshtokenExpirationTime);

                user.TokenExpirationTime = DateTime.Now.AddMinutes(tokenExpirationTime);
                user.RefreshToken = refeshToken;
                user.RefreshTokenExpirationTime = DateTime.Now.AddMinutes(refeshtokenExpirationTime);
                await _context.SaveChangesAsync();
                await _userManager.UpdateAsync(user);

                var response = new TokenResponse(user.Id, user.UserName, token, refeshToken, StatusCodes.Status200OK, "Login Success", DateTime.Now.AddMinutes(tokenExpirationTime), DateTime.Now.AddMinutes(refeshtokenExpirationTime));
                return response;
            }
            else
            {
                int accessFailedCount = await _userManager.GetAccessFailedCountAsync(user);
                if (accessFailedCount >= 2)
                {
                    user.TwoFactorEnabled = true;
                }
                await _context.SaveChangesAsync();
                await _userManager.UpdateAsync(user);
                result.StatusCode = StatusCodes.Status401Unauthorized;
                result.Message = "Invalid Password";
                return result;
            }
        }

        public async Task<TokenResponse> AuthenticationOTP(AuthenticationOTPRequest request)
        {
            var result = new TokenResponse();
            var user = await _articleUserRepository.GetUserByUsername(request.userName);
            var singIn = await _signInManager.TwoFactorSignInAsync("Email", request.code, false, false);

            if (singIn.Succeeded)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                user.TwoFactorEnabled = false;
                if (user != null)
                {
                    var claimUser = new ClaimUserLogin()
                    {
                        Id = user.Id,
                        UserName = request.userName,
                        Email = user.Email,
                        Roles = userRole
                    };

                    var token = _tokenService.GenerateAccessToken(claimUser);
                    var refeshToken = _tokenService.GenerateRefreshToken();
                    var timeToken = int.TryParse(_config["JWT:TokenExpirationTime"], out int tokenExpirationTime);
                    var timeRefeshToken = int.TryParse(_config["JWT:RefreshTokenExpirationTime"], out int refeshtokenExpirationTime);

                    user.TokenExpirationTime = DateTime.Now.AddMinutes(tokenExpirationTime);
                    user.RefreshToken = refeshToken;
                    user.RefreshTokenExpirationTime = DateTime.Now.AddMinutes(refeshtokenExpirationTime);
                    await _context.SaveChangesAsync();
                    await _userManager.UpdateAsync(user);

                    var response = new TokenResponse(user.Id, user.UserName, token, refeshToken, StatusCodes.Status200OK, "Login success", DateTime.Now.AddMinutes(tokenExpirationTime), DateTime.Now.AddMinutes(refeshtokenExpirationTime));
                    return response;
                }
            }
            else
            {
                result.StatusCode = StatusCodes.Status401Unauthorized;
                result.Message = "Invalid OTP";
            }
            return result;
        }

        public async Task<ResponseMessage> ForgotPassword(string EmailForgotPassword)
        {
            var result = new ResponseMessage();
            var user = await _articleUserRepository.GetUserByEmail(EmailForgotPassword);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var Address = "https://localhost:7099/swagger/index.html";
                var forgotPasswordLink = $"{Address}?token={token}&email={EmailForgotPassword}";
                var message = _emailService.ChangeToMessageEmail(user.Email, "Forgot Password Link", forgotPasswordLink!);
                result = await _emailService.SendEmail(message);
                return result;
            }
            else
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Message = "No Email Found";
            }
            return result;
        }

        public async Task<ResponseMessage> ResetPassword(ResetPassword resetPassword)
        {
            var result = new ResponseMessage();
            var user = await _articleUserRepository.GetUserByEmail(resetPassword.Email);
            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.ConfirmPassword);
                if (resetPassResult.Succeeded)
                {
                    result.StatusCode = StatusCodes.Status200OK;
                    result.Message = "Change Password Success";
                }
                else
                {
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    result.Message = "Change Password Fail";
                }
            }
            else
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Message = "No user found";
            }
            return result;
        }

        public async Task<ResponseMessage> ChangePassword(ChangePassword changePassword)
        {
            var result = new ResponseMessage();
            var user = await _articleUserRepository.GetUserByUsername(changePassword.UserName);
            if (user == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Message = "User not found.";
                return result;
            }
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, changePassword.PasswordOld);
            if (!isPasswordCorrect)
            {
                result.StatusCode = StatusCodes.Status401Unauthorized;
                result.Message = "Incorrect old password.";
                return result;
            }
            var changeResult = await _userManager.ChangePasswordAsync(user, changePassword.PasswordOld, changePassword.PasswordNew);
            if (!changeResult.Succeeded)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "Failed to change password.";
                return result;
            }
            result.StatusCode = StatusCodes.Status200OK;
            result.Message = "Password changed successfully.";
            return result;
        }

        public async Task<ResponseMessage> SendEmailConfirm(string email)
        {
            ResponseMessage responseMessage = new();
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // note
                //await _userManager.ConfirmEmailAsync(user, token);

                var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
                var confirmationLink = urlHelper.Action(
                     "ConfirmEmail",
                     "ArticleAppUser",
                     new { token, email },
                     _actionContextAccessor.ActionContext.HttpContext.Request.Scheme
                 );

                var message = _emailService.ChangeToMessageEmail(
                    email, "Email Confirmation Link",
                    $"Please confirm your email by clicking the link: <a href='{confirmationLink}'>Confirm Email</a>"
                );
                var resultSendEmail = await _emailService.SendEmail(message);
                return resultSendEmail;
            }
            else
            {
                responseMessage.Message = "No email found";
                responseMessage.StatusCode = StatusCodes.Status404NotFound;
            }
            return responseMessage;
        }

        public async Task<List<ArticleAppUser>> GetAllUser()
        {
            var listUser = await _userManager.Users.ToListAsync();
            return listUser;
        }

        public async Task<PagedList<UserDto>> GetPaging(PageRequest request)
        {
            var pagedList = await _articleUserRepository.GetPaging(request);
            var mappedItems = _mapper.Map<List<UserDto>>(pagedList.TotalItems);
            return new PagedList<UserDto>(mappedItems, pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalPages, pagedList.StartPage, pagedList.EndPage);
        }

        public async Task<IList<string>> GetAllRoleByName(string UserName)
        {
            return await _articleUserRepository.GetAllRoleByName(UserName);
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _articleUserRepository.GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserByName(string UserName)
        {
            var user = await _articleUserRepository.GetUserByUsername(UserName);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<ResponseMessage> Register(RegisterRequest request)
            {
            ResponseMessage responseMessage = new();
            var findUserName = await _articleUserRepository.GetUserByUsername(request.UserName);
            var findEmail = await _articleUserRepository.GetUserByEmail(request.Email);
            if (findUserName != null)
            {
                responseMessage.StatusCode = StatusCodes.Status409Conflict;
                responseMessage.Message = "UserName has been used";
                return responseMessage;
            }
            else if (findEmail != null)
            {
                responseMessage.StatusCode = StatusCodes.Status409Conflict;
                responseMessage.Message = "Email has been used";
                return responseMessage;
            }
            else
            {
                var user = new ArticleAppUser()
                {
                    DateCreated = DateTime.Now,
                    UserName = request.UserName,
                    PasswordHash = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Adress = request.Adress,
                };
                if (request.UploadFile != null)
                {
                    var uploadAvatar = await _awsS3Service.UploadFile(request.UploadFile, _configuration["Bucket:User"], request.LastName + request.FirstName);
                    if (uploadAvatar.StatusCode == StatusCodes.Status200OK)
                    {
                        user.Avatar = uploadAvatar.PresignedUrl;
                    }
                    else
                    {
                        responseMessage.StatusCode = uploadAvatar.StatusCode;
                        responseMessage.Message = uploadAvatar.Message;
                        return responseMessage;
                    }
                }
                try
                {
                    var create = await _userManager.CreateAsync(user, request.Password);
                    if (create.Succeeded)
                    {
                        responseMessage.Message = "Register Success";
                        responseMessage.StatusCode = StatusCodes.Status200OK;
                    }
                    else
                    {
                        var errors = string.Join(", ", create.Errors.Select(e => e.Description));
                        responseMessage.Message = $"Register Fail: {errors}";
                        responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                }
                catch (Exception ex)
                {
                    responseMessage.Message = $"An unexpected error occurred: {ex.Message}";
                    responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                }

            }
            return responseMessage;
        }

        public async Task<ResponseMessage> RegisterManager(ManagerRegister request)
        {
            ResponseMessage responseMessage = new();
            var findUserName = await _articleUserRepository.GetUserByUsername(request.UserName);
            var findEmail = await _articleUserRepository.GetUserByEmail(request.Email);
            if (findUserName != null)
            {
                responseMessage.StatusCode = StatusCodes.Status409Conflict;
                responseMessage.Message = "UserName has been used";
                return responseMessage;
            }
            else if (findEmail != null)
            {
                responseMessage.StatusCode = StatusCodes.Status409Conflict;
                responseMessage.Message = "Email has been used";
                return responseMessage;
            }
            else
            {
                var user = new ArticleAppUser()
                {
                    DateCreated = DateTime.Now,
                    UserName = request.UserName,
                    PasswordHash = request.PassWord,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Adress = request.Adress,
                };
                if (request.UploadFile != null)
                {
                    var uploadAvatar = await _awsS3Service.UploadFile(request.UploadFile, _configuration["Bucket:User"], request.LastName + request.FirstName);
                    if (uploadAvatar.StatusCode == StatusCodes.Status200OK)
                    {
                        user.Avatar = uploadAvatar.PresignedUrl;
                    }
                    else
                    {
                        responseMessage.StatusCode = uploadAvatar.StatusCode;
                        responseMessage.Message = uploadAvatar.Message;
                        return responseMessage;
                    }
                }

                var createUser = await _userManager.CreateAsync(user, request.PassWord);
                if (createUser.Succeeded)
                {
                    if (request.Roles != null)
                    {
                        var usercontent = await _articleUserRepository.GetUserByUsername(request.UserName);
                        var createrole = await _userManager.AddToRolesAsync(usercontent, request.Roles);
                    }
                    responseMessage.Message = "Register Success";
                    responseMessage.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    responseMessage.Message = "Register Fail";
                    responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }
            return responseMessage;
        }

        public async Task RemoveListUserByName(string value)
        {
            var list = JsonConvert.DeserializeObject<List<string>>(value);
            foreach (var sub in list)
            {
                var user = await _articleUserRepository.GetUserByUsername(sub);
                await _articleUserRepository.DeleteUser(user);
            }
        }

        // (bấm lưu)thêm quyền cho các tk 
        public async Task<bool> RoleAssign(List<RoleAssignRequest> request)
        {
            var listTrue = request.Where(x => x.Status == true).ToList();
            foreach (var sub in listTrue)
            {
                var user = await _articleUserRepository.GetUserByUsername(sub.UserName);
                var role = await _roleManager.FindByNameAsync(sub.Name);
                await _userManager.AddToRoleAsync(user, role.ToString());
            }

            var listFalse = request.Where(x => x.Status == false).ToList();
            foreach (var sub in listFalse)
            {
                var user = await _articleUserRepository.GetUserByUsername(sub.UserName);
                var role = await _roleManager.FindByNameAsync(sub.Name);
                await _userManager.RemoveFromRoleAsync(user, role.ToString());
            }
            return true;
        }

        public async Task<bool> UpdateUser(ArticleAppUser request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Adress = request.Adress;

            var update = await _articleUserRepository.UpdateUser(user);
            return update;
        }
    }
}
