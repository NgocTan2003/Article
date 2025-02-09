using Article.Dtos.UserDto;

namespace Article.WebApp.ConnectAPI.ArticleTokenCnAPI
{
    public interface IArticleTokenConnectAPI
    {
        Task<TokenResponse> RefreshToken(TokenApiModel tokenApiModel);
    }
}
