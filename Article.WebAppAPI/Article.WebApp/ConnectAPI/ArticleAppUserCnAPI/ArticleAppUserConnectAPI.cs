using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.Categories;
using Article.Dtos.UserDto;
using Article.WebApp.ConnectAPI.ArticleAppUserCnAPI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace Article.WebApp.ConnectAPI.ArticleAppUser
{
    public class ArticleAppUserConnectAPI : IArticleAppUserConnectAPI
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleAppUserConnectAPI(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient("AuthClient"); 
        }

        public async Task<TokenResponse> Authentication(AuthenticationRequest request)
        {
            var authen = await _httpClient.PostAsJsonAsync("/api/ArticleAppUser/Authen", request);
            if (!authen.IsSuccessStatusCode)
            {
                return new TokenResponse
                {
                    Message = "Lỗi kết nối đến máy chủ",
                    StatusCode = (int)authen.StatusCode
                };
            }

            var readString = await authen.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TokenResponse>(readString);

            if (response == null || response.StatusCode != StatusCodes.Status200OK)
            {
                return new TokenResponse
                {
                    Message = response.Message,
                    StatusCode = response?.StatusCode ?? StatusCodes.Status400BadRequest
                };
            }

            _httpContextAccessor.HttpContext.Session.SetString("AccessToken", response.AccessToken);
            _httpContextAccessor.HttpContext.Session.SetString("RefreshToken", response.RefreshToken);
            _httpContextAccessor.HttpContext.Session.SetString("UserName", response.UserName);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, response.UserName) };

            //if (!string.IsNullOrEmpty(response.Roles))
            //{
            //    foreach (var role in response.Roles.Split(";"))
            //    {
            //        claims.Add(new Claim(ClaimTypes.Role, role));
            //    }
            //}

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Ghi nhớ đăng nhập
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
            return response;
        }

        public Task<TokenResponse> AuthenticationOTP(AuthenticationOTPRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> ChangePassword(ChangePassword changePassword)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> ForgotPassword(string EmailForgotPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDto>> GetPaging(PageRequest request)
        {
            var allUser = await _httpClient.GetFromJsonAsync<List<UserDto>>("/api/ArticleAppUser/GetAll");
            return allUser;
        }

        public Task<IList<string>> GetAllRoleByName(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDto>> GetAll()
        {
            var allUser = await _httpClient.GetFromJsonAsync<List<UserDto>>("/api/ArticleAppUser/GetAll");
            return allUser;
        }

        public Task<UserDto> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserByName(string UserName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> RegisterManager(ManagerRegister request)
        {
            throw new NotImplementedException();
        }

        public Task RemoveListUserByName(string value)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> ResetPassword(ResetPassword resetPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RoleAssign(List<RoleAssignRequest> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> SendEmailConfirm(string email)
        {
            throw new NotImplementedException();
        }

        //public Task<bool> UpdateUser(ArticleAppUser request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
