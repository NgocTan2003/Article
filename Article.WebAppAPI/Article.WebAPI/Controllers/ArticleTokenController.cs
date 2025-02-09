using Article.Application.Services.Interfaces;
using Article.Dtos.UserDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleTokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public ArticleTokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("RefreshToken")]
        public async Task<TokenResponse> Refresh([FromBody] TokenApiModel tokenApiModel)
        {
            var result = new TokenResponse();
            try
            {
                result = await _tokenService.RefreshToken(tokenApiModel);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = StatusCodes.Status500InternalServerError;
            }
            return result;
        }
    }
}
