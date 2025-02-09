using Article.Common.Seedwork;
using Article.Dtos.ArticleRoleDto;
using Article.WebApp.ConnectAPI.ArticleRoleCnAPI;
using Article.WebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Article.WebApp.Controllers
{
    public class ArticleRoleController : Controller
    {
        private readonly IArticleAppRoleConnectAPI _articleAppRoleConnectAPI;
        private readonly IMapper _mapper;

        public ArticleRoleController(IArticleAppRoleConnectAPI articleAppRoleConnectAPI, IMapper mapper)
        {
            _articleAppRoleConnectAPI = articleAppRoleConnectAPI;
            _mapper = mapper;
        }

        //public async Task<IActionResult> Index(int pg = 1, string searchText = "")
        //{
        //    var pageRequest = new PageRequest(pg, null, searchText);
        //    var response = await _articleAppRoleConnectAPI.GetPaging(pageRequest);
        //    if (response.StatusCode == StatusCodes.Status200OK)
        //    {
        //        var page = new Paginate(response.Items.PageSize, response.Items.CurrentPage, response.Items.TotalPages, response.Items.StartPage, response.Items.EndPage);
        //        ViewBag.Page = page;
        //        ViewBag.SearchText = searchText;
        //        return View(response.Items.TotalItems);
        //    }
        //    else if (response.StatusCode == StatusCodes.Status401Unauthorized)
        //    {
        //        return RedirectToAction("Login", "Authentication");
        //    }
        //    else
        //    {
        //        TempData["Error"] = response.Message;
        //        return View();
        //    }
        //}

        public async Task<IActionResult> Index(int pg = 1, string searchText = "")
        {
            var pageRequest = new PageRequest(pg, null, searchText);
            var response = await _articleAppRoleConnectAPI.GetPaging(pageRequest);

            if (response.StatusCode == StatusCodes.Status200OK)
            {
                var page = new Paginate(response.Items.PageSize, response.Items.CurrentPage, response.Items.TotalPages, response.Items.StartPage, response.Items.EndPage);
                ViewBag.Page = page;
                ViewBag.SearchText = searchText;
                return View(response.Items.TotalItems);
            }

            TempData["Error"] = response.Message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArticleRole request)
        {
            var response = await _articleAppRoleConnectAPI.CreateRole(request);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = response.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var role = await _articleAppRoleConnectAPI.FindRoleById(Id);
            var updateRole = _mapper.Map<UpdateArticleRole>(role);
            return View(updateRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateArticleRole request)
        {
            var response = await _articleAppRoleConnectAPI.UpdateRole(request);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = response.Message;
                return View();
            }
        }

        public async Task<IActionResult> Detail(string Id)
        {
            var detailRole = await _articleAppRoleConnectAPI.FindRoleById(Id);
            return View(detailRole);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            var response = await _articleAppRoleConnectAPI.DeleteRole(Id);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = response.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
