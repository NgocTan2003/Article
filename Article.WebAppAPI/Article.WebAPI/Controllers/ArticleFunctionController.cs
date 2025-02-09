using Article.Application.Services.Implements;
using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Article.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleFunctionController : ControllerBase
    {
        private readonly IArticleFunctionService _articleFunctionService;

        public ArticleFunctionController(IArticleFunctionService articleFunctionService)
        {
            _articleFunctionService = articleFunctionService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var functions = _articleFunctionService.GetAll();
                return Ok(functions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message });
            }
        }

    }
}
