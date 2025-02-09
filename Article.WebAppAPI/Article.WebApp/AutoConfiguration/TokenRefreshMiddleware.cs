using Article.Dtos.UserDto;
using Article.WebApp.ConnectAPI.ArticleTokenCnAPI;
using System.IdentityModel.Tokens.Jwt;

namespace Article.WebApp.AutoConfiguration
{
    public class TokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IArticleTokenConnectAPI _articleTokenConnectAPI;

        public TokenRefreshMiddleware(RequestDelegate next, IArticleTokenConnectAPI articleTokenConnectAPI)
        {
            _next = next;
            _articleTokenConnectAPI = articleTokenConnectAPI;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Nếu là các request không liên quan đến xác thực, tiếp tục xử lý bình thường
            if (path == "/authentication/login" || path.StartsWith("/css") || path.StartsWith("/js"))
            {
                await _next(context);
                return;
            }

            // Lấy access token và refresh token từ session
            var accessToken = context.Session.GetString("AccessToken");
            var refreshToken = context.Session.GetString("RefreshToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                // Nếu không có access token, chuyển đến trang đăng nhập
                context.Response.Redirect("/Authentication/Login");
                return;
            }

            // Kiểm tra xem token có hết hạn không
            if (IsTokenExpired(accessToken))
            {
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var tokenAPI = new TokenApiModel
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };

                    var tokenResponse = await _articleTokenConnectAPI.RefreshToken(tokenAPI);

                    if (tokenResponse.StatusCode == StatusCodes.Status200OK)
                    {
                        // Nếu refresh token thành công, cập nhật session với token mới
                        context.Session.SetString("AccessToken", tokenResponse.AccessToken);
                        context.Session.SetString("RefreshToken", tokenResponse.RefreshToken);

                        // Đính kèm token mới vào header của request
                        context.Request.Headers["Authorization"] = $"Bearer {tokenResponse.AccessToken}";

                        // Thực hiện lại request với token mới
                        await _next(context);
                        return;
                    }
                    else
                    {
                        // Nếu refresh token thất bại, chuyển hướng về trang đăng nhập
                        context.Response.Redirect("/Authentication/Login");
                        return;
                    }
                }
                else
                {
                    // Nếu không có refresh token, chuyển về trang đăng nhập
                    context.Response.Redirect("/Authentication/Login");
                    return;
                }
            }

            // Nếu token không hết hạn, tiếp tục gửi request bình thường
            context.Request.Headers["Authorization"] = $"Bearer {accessToken}";

            // Tiến hành xử lý request và response
            var originalResponseStream = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context);

                memoryStream.Position = 0;

                // Nếu response trả về 401 (Unauthorized) và có refresh token, thực hiện lại cơ chế refresh token
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized && !string.IsNullOrEmpty(refreshToken))
                {
                    var tokenAPI = new TokenApiModel
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };

                    var tokenResponse = await _articleTokenConnectAPI.RefreshToken(tokenAPI);

                    if (tokenResponse.StatusCode == StatusCodes.Status200OK)
                    {
                        context.Session.SetString("AccessToken", tokenResponse.AccessToken);
                        context.Session.SetString("RefreshToken", tokenResponse.RefreshToken);
                        context.Request.Headers["Authorization"] = $"Bearer {tokenResponse.AccessToken}";

                        // Redirect lại để gửi request mới với token mới
                        context.Response.Redirect(context.Request.Path);
                        return;
                    }
                    else
                    {
                        context.Response.Redirect("/Authentication/Login");
                        return;
                    }
                }

                // Nếu không có lỗi 401, tiếp tục trả kết quả về cho client
                await memoryStream.CopyToAsync(originalResponseStream);
            }
        }

        public bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            return jsonToken?.ValidTo < DateTime.UtcNow;
        }
    }
}
