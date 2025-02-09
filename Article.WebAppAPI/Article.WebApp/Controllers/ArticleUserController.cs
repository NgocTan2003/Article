using Article.Dtos.UserDto;
using Article.WebApp.ConnectAPI.ArticleAppUserCnAPI;
using Article.WebApp.ConnectAPI.ArticleRoleCnAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Article.WebApp.Controllers
{
    public class ArticleUserController : Controller
    {
        private readonly IArticleAppUserConnectAPI _articleAppUser;
        private readonly IArticleAppRoleConnectAPI _articleAppRole;

        public ArticleUserController(IArticleAppUserConnectAPI articleAppUser, IArticleAppRoleConnectAPI articleAppRole)
        {
            _articleAppUser = articleAppUser;
            _articleAppRole = articleAppRole;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _articleAppUser.GetAll();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _articleAppRole.GetAll();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            var model = new ManagerRegister();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagerRegister request)
        {
            var response = await _articleAppUser.RegisterManager(request);
            return View(response);
        }
    }
}
