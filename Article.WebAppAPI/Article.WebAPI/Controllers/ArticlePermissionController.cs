using Article.Application.Services.Implements;
using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Dtos.ArticlePermissionDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlePermissionController : ControllerBase
    {
        private readonly IArticlePermissionService _articlePermissionService;

        public ArticlePermissionController(IArticlePermissionService articlePermissionService)
        {
            _articlePermissionService = articlePermissionService;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var permissions = _articlePermissionService.GetAll();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateArticlePermission request)
        {
            try
            {
                var create = _articlePermissionService.CreateListArticlePermission(request);
                return Ok(create);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("UpdateContent")]
        public async Task<IActionResult> UpdateContent([FromBody] UpdateArticlePermission request)
        {
            try
            {
                var update = _articlePermissionService.UpdateContent(request);
                return Ok(update);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

    }
}
