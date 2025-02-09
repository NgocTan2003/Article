using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleCategoryDto;
using Article.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCategoryController : ControllerBase
    {
        private readonly IArticleCategoryService _articleCategoryService;

        public ArticleCategoryController(IArticleCategoryService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _articleCategoryService.GetAll();
                return Ok(new ResponseListItem<ArticleCategoryDto>() { Items = categories, Message = "Success", StatusCode = StatusCodes.Status200OK });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [HttpGet("GetCategoriesWithSubCategories")]
        public async Task<IActionResult> GetCategoriesWithSubCategories()
        {
            try
            {
                var categories = await _articleCategoryService.GetCategoriesWithSubCategories();
                return Ok(new ResponseListItem<ArticleCategoryDto>() { Items = categories, Message = "Success", StatusCode = StatusCodes.Status200OK });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [HttpPost("GetPaging")]
        public async Task<IActionResult> GetPaging([FromBody] PageRequest request)
        {
            try
            {
                var categories = await _articleCategoryService.GetPaging(request);
                return Ok(new ResponsePageListItem<ArticleCategoryDto>() { Items = categories, Message = "Success", StatusCode = StatusCodes.Status200OK });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateArticleCategory([FromBody] CreateArticleCategory request)
        {
            try
            {
                var create = _articleCategoryService.CreateArticleCategory(request);
                return Ok(create);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateArticleCategory([FromBody] UpdateArticleCategory request)
        {
            try
            {
                var update = _articleCategoryService.UpdateArticleCategory(request);
                return Ok(update);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteArticleCategory([FromBody] Guid id)
        {
            try
            {
                var delete = _articleCategoryService.DeleteArticleCategory(id);
                return Ok(delete);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }
    }
}
