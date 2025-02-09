using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleCategoryDto;
using Article.Dtos.ArticleSubCategoryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleSubCategoryController : ControllerBase
    {
        private readonly IArticleSubCategoryService _articleSubCategoryService;

        public ArticleSubCategoryController(IArticleSubCategoryService articleSubCategoryService)
        {
            _articleSubCategoryService = articleSubCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subCategories = _articleSubCategoryService.GetAll();
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [HttpPost("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromBody] PageRequest request)
        {
            try
            {
                //var subCategories = _articleSubCategoryService.GetAllPaging(request);
                //return Ok(subCategories);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateArticleSubCategory([FromBody] CreateArticleSubCategory request)
        {
            try
            {
                var create = _articleSubCategoryService.CreateArticleSubCategory(request);
                return Ok(create);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateArticleSubCategory([FromBody] UpdateArticleSubCategory request)
        {
            try
            {
                var update = _articleSubCategoryService.UpdateArticleSubCategory(request);
                return Ok(update);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteArticleSubCategory([FromBody] Guid id)
        {
            try
            {
                var delete = _articleSubCategoryService.DeleteArticleSubCategory(id);
                return Ok(delete);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

    }
}
