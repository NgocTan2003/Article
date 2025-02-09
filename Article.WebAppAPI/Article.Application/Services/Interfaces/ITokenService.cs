using Article.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(ClaimUserLogin request);
        string GenerateRefreshToken();
        Task<TokenResponse> RefreshToken(TokenApiModel tokenApiModel);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
