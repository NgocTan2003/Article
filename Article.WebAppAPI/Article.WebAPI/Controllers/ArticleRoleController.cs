using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleRoleDto;
using Article.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleRoleController : ControllerBase
    {
        private readonly IArticleRoleService _articleRoleService;

        public ArticleRoleController(IArticleRoleService articleRoleService)
        {
            _articleRoleService = articleRoleService;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await _articleRoleService.GetAll();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("GetPaging")]
        public async Task<IActionResult> GetPaging([FromBody] PageRequest request)
        {
            try
            {
                var categories = await _articleRoleService.GetPaging(request);
                return Ok(new ResponsePageListItem<ArticleRoleDto>() { Items = categories, Message = "Success", StatusCode = StatusCodes.Status200OK });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("FindRoleById")]
        public async Task<IActionResult> FindRoleById([FromQuery] string Id)
        {
            try
            {
                var role = await _articleRoleService.FindRoleById(Id);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateArticleRole request)
        {
            try
            {
                var create = await _articleRoleService.CreateRole(request);
                return Ok(create);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateArticleRole request)
        {
            try
            {
                var update = await _articleRoleService.UpdateRole(request);
                return Ok(update);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteRole([FromQuery] string Id)
        {
            try
            {
                var delete = await _articleRoleService.DeleteRole(Id);
                return Ok(delete);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }
    }
}
