using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleRoleDto;
using Article.Dtos.Roles;
using Article.WebApp.ConnectAPI.ArticleRoleCnAPI;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Article.WebApp.ConnectAPI.ArticleRole
{
    public class ArticleAppRoleConnectAPI : IArticleAppRoleConnectAPI
    {
        private readonly HttpClient _httpClient;

        public ArticleAppRoleConnectAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");  
        }

        public async Task<List<ArticleRoleDto>> GetAll()
        {
            var allRole = await _httpClient.GetFromJsonAsync<List<ArticleRoleDto>>("/api/ArticleRole/GetAll");
            return allRole;
        }

        public async Task<ResponsePageListItem<ArticleRoleDto>> GetPaging(PageRequest request)
        {
            var allRolePaging = await _httpClient.PostAsJsonAsync("/api/ArticleRole/GetPaging", request);
            if (allRolePaging.IsSuccessStatusCode)
            {
                var readstring = await allRolePaging.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponsePageListItem<ArticleRoleDto>>(readstring);
                return response;
            }
            else if (allRolePaging.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResponsePageListItem<ArticleRoleDto>
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            return null;
        }

        public async Task<ArticleRoleDto> FindRoleById(string Id)
        {
            var get = await _httpClient.PostAsync($"/api/ArticleRole/FindRoleById?Id={Id}", null);
            var readstring = await get.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ArticleRoleDto>(readstring);
            return response;
        }

        public async Task<ResponseMessage> CreateRole(CreateArticleRole request)
        {
            var create = await _httpClient.PostAsJsonAsync("/api/ArticleRole/Create", request);
            var readstring = await create.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseMessage>(readstring);
            return response;
        }

        public async Task<ResponseMessage> UpdateRole(UpdateArticleRole request)
        {
            var create = await _httpClient.PostAsJsonAsync("/api/ArticleRole/Update", request);
            var readstring = await create.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseMessage>(readstring);
            return response;
        }

        public async Task<ResponseMessage> DeleteRole(string Id)
        {
            var create = await _httpClient.PostAsync($"/api/ArticleRole/Delete?Id={Id}", null);
            var readstring = await create.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseMessage>(readstring);
            return response;
        }
    }
}
