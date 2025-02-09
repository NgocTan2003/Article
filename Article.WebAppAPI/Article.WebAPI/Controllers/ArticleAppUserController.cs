using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Data.Entity;
using Article.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleAppUserController : ControllerBase
    {
        private readonly IArticleUserService _articleUserService;
        private readonly UserManager<ArticleAppUser> _userManager;

        public ArticleAppUserController(IArticleUserService articleUserService, UserManager<ArticleAppUser> userManager)
        {
            _articleUserService = articleUserService;
            _userManager = userManager;
        }

        [HttpPost("Authen")]
        [AllowAnonymous]
        public async Task<IActionResult> Authen([FromBody] AuthenticationRequest request)
        {
            try
            {
                var auth = await _articleUserService.Authentication(request);
                return Ok(auth);
            }
            catch (Exception ex)
            {
                return BadRequest(new TokenResponse() { AccessToken = null, RefreshToken = null, Message = ex.Message, UserName = null });
            }
        }

        [HttpPost("AuthenOTP")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenOTP([FromBody] AuthenticationOTPRequest request)
        {
            try
            {
                var auth = await _articleUserService.AuthenticationOTP(request);
                return Ok(auth);
            }
            catch (Exception ex)
            {
                return BadRequest(new TokenResponse() { AccessToken = null, RefreshToken = null, Message = ex.Message, UserName = null });
            }
        }

        // bắt event khi user bấm vào link xác nhận email 
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new ResponseMessage { StatusCode = StatusCodes.Status200OK, Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                                   new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = "This User Doenot exist" });
        }

        [HttpPost("SendEmailForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string emailForgotPassword)
        {
            try
            {
                var response = await _articleUserService.ForgotPassword(emailForgotPassword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        // sau khi ForgotPassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            try
            {
                var response = await _articleUserService.ResetPassword(resetPassword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            try
            {
                var response = await _articleUserService.ChangePassword(changePassword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        // gửi email verified email 
        [HttpGet("SendEmailConfirm")]
        public async Task<IActionResult> SendEmailConfirm([FromQuery] string email)
        {
            try
            {
                var result = await _articleUserService.SendEmailConfirm(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _articleUserService.GetAllUser();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = 500, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("GetPaging")]
        public async Task<IActionResult> GetPaging([FromBody] PageRequest request)
        {
            try
            {
                var user = await _articleUserService.GetPaging(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("GetAllRoleByName")]
        public async Task<IActionResult> GetAllRoleByName([FromBody] string userName)
        {
            try
            {
                var result = await _articleUserService.GetAllRoleByName(userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("FindUserById")]
        public async Task<IActionResult> FindUserById([FromBody] string id)
        {
            try
            {
                var user = await _articleUserService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("FindUserByName")]
        public async Task<IActionResult> FindUserByName([FromBody] string userName)
        {
            try
            {
                var user = await _articleUserService.GetUserByName(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _articleUserService.Register(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [HttpPost("RegisterManager")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterManager([FromBody] ManagerRegister request)
        {
            try
            {
                var result = await _articleUserService.RegisterManager(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("RoleAssign")]
        public async Task<IActionResult> RoleAssign([FromBody] List<RoleAssignRequest> request)
        {
            try
            {
                var result = await _articleUserService.RoleAssign(request);
                return Ok(new ResponseMessage("Success"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] ArticleAppUser request)
        {
            try
            {
                var update = await _articleUserService.UpdateUser(request);
                return Ok(update);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }

        }

        [Authorize]
        [HttpPost("RemoveListUserByName")]
        public async Task<IActionResult> RemoveListUserByName([FromBody] string userName)
        {
            try
            {
                await _articleUserService.RemoveListUserByName(userName);
                return Ok(new ResponseMessage("Success"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }
    }
}
