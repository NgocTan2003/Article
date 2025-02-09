using Article.Common.Seedwork;
using Article.WebApp.ConnectAPI.ArticleCategoryCnAPI;
using Article.WebApp.ConnectAPI.ArticleRoleCnAPI;
using Article.WebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Article.WebApp.Controllers
{
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryConnectAPI _articleCategoryConnectAPI;
        private readonly IMapper _mapper;

        public ArticleCategoryController(IArticleCategoryConnectAPI articleCategoryConnectAPI, IMapper mapper)
        {
            _articleCategoryConnectAPI = articleCategoryConnectAPI;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pg = 1, string searchText="")
        {
            var pageRequest = new PageRequest(pg, null, searchText);
            var response = await _articleCategoryConnectAPI.GetPaging(pageRequest);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                var page = new Paginate(response.Items.PageSize, response.Items.CurrentPage, response.Items.TotalPages, response.Items.StartPage, response.Items.EndPage);
                ViewBag.Page = page;
                ViewBag.SearchText = searchText;
                return View(response.Items.TotalItems);
            }
            else
            {
                TempData["Error"] = response.Message;
                return View();
            }
        }
    }
}
