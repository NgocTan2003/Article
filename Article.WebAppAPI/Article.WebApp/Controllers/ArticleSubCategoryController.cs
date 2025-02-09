using Microsoft.AspNetCore.Mvc;

namespace Article.WebApp.Controllers
{
    public class ArticleSubCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
