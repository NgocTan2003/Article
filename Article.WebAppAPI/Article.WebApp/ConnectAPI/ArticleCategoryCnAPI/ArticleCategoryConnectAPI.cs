using Amazon.Runtime;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.Categories;
using Article.Dtos.Roles;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http;

namespace Article.WebApp.ConnectAPI.ArticleCategoryCnAPI
{
    public class ArticleCategoryConnectAPI : IArticleCategoryConnectAPI
    {
        private readonly HttpClient _httpClient;

        public ArticleCategoryConnectAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<ResponsePageListItem<ArticleCategoryDto>> GetPaging(PageRequest request)
        {
            var allCategoryPaging = await _httpClient.PostAsJsonAsync("/api/ArticleCategory/GetPaging", request);
            var readstring = await allCategoryPaging.Content.ReadAsStringAsync();
            if (allCategoryPaging.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<ResponsePageListItem<ArticleCategoryDto>>(readstring);
                return response;
            }
            else
            {
                return new ResponsePageListItem<ArticleCategoryDto>
                {
                    StatusCode = (int)allCategoryPaging.StatusCode,
                    Message = readstring
                };
            }
        }



    }
}
