using Article.Common.ReponseBase;
using Article.Dtos.UserDto;
using System.Linq.Dynamic.Core.Tokenizer;

namespace Article.WebApp.ConnectAPI.ArticleTokenCnAPI
{
    public class ArticleTokenConnectAPI : IArticleTokenConnectAPI
    {
        private readonly HttpClient _httpClient;

        public ArticleTokenConnectAPI(IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }
            _httpClient = httpClientFactory.CreateClient("AuthClient");
        }

        public async Task<TokenResponse> RefreshToken(TokenApiModel tokenApiModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/ArticleToken/RefreshToken", tokenApiModel);
            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return tokenResponse;
            }
            return null;
        }
    }
}
