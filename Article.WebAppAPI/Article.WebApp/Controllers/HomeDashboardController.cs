using Article.Common.ReponseBase;
using Article.Dtos.Categories;
using Article.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Article.WebApp.Controllers
{
    public class HomeDashboardController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeDashboardController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            //var getall = await _httpClient.GetFromJsonAsync<ResponseListItem<ArticleCategoryDto>>("/api/ArticleCategory/GetAll");
            //return getall;

            return View();
        }
    }
}
