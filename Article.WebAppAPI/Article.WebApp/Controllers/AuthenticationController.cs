using Article.Dtos.UserDto;
using Article.WebApp.ConnectAPI.ArticleAppUser;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Article.WebApp.ConnectAPI.ArticleAppUserCnAPI;

namespace Article.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IArticleAppUserConnectAPI _articleAppUserConnectAPI;

        public AuthenticationController(IArticleAppUserConnectAPI articleAppUserConnectAPI)
        {
            _articleAppUserConnectAPI = articleAppUserConnectAPI;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            var result = await _articleAppUserConnectAPI.Authentication(request);
            if (result.StatusCode == StatusCodes.Status200OK)
            {
                return RedirectToAction("Index", "HomeDashboard");
            }

            TempData["Error"] = result.Message;
            return View();
        }


    }
}
