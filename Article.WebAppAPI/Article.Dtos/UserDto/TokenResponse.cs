using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.UserDto
{
    public class TokenResponse
    {
        public string? Id { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserName { get; set; }
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public DateTime TokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        public TokenResponse()
        {

        }

        // Constructor
        public TokenResponse(string? id, string? userName, string? accessToken, string? refreshToken,
                             int? statusCode, string? message, DateTime tokenExpiration, DateTime refreshTokenExpiration)
        {
            Id = id;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserName = userName;
            StatusCode = statusCode;
            Message = message;
            TokenExpiration = tokenExpiration;
            RefreshTokenExpiration = refreshTokenExpiration;
        }
    }
}
