using Article.Application.Repositories.Implements;
using Article.Application.Repositories.Interfaces;
using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Data.Entity;
using Article.Dtos.ArticleCategoryDto;
using Article.Dtos.Categories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Article.Application.Services.Implements
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleSubCategoryRepository _articleSubCategoryRepository;
        private readonly IAwsS3Service _awsS3Service;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ArticleCategoryService(IArticleCategoryRepository articleCategoryRepository, IAwsS3Service awsS3Service, IConfiguration configuration, IMapper mapper, IArticleSubCategoryRepository articleSubCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _articleSubCategoryRepository = articleSubCategoryRepository;
            _awsS3Service = awsS3Service;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<ArticleCategoryDto>> GetAll()
        {
            var categories = _articleCategoryRepository.GetAllAsNoTracking();

            var categoriesDto = _mapper.Map<List<ArticleCategory>, List<ArticleCategoryDto>>(categories.ToList());
            return categoriesDto;
        }

        public async Task<List<ArticleCategoryDto>> GetCategoriesWithSubCategories()
        {
            var categories = await _articleCategoryRepository.GetCategoriesWithSubCategories();
            var categoriesDto = _mapper.Map<List<ArticleCategory>, List<ArticleCategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task<PagedList<ArticleCategoryDto>> GetPaging(PageRequest request)
        {
            var pagedList = await _articleCategoryRepository.GetPaging(request);
            var mappedItems = _mapper.Map<List<ArticleCategoryDto>>(pagedList.TotalItems);
            return new PagedList<ArticleCategoryDto>(mappedItems, pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalPages, pagedList.StartPage, pagedList.EndPage);
        }

        public async Task<ResponseMessage> CreateArticleCategory(CreateArticleCategory request)
        {
            ResponseMessage responseMessage = new();
            var category = new ArticleCategory()
            {
                Name = request.Name,
                Description = request.Description,
                IsDelete = false,
                CreateBy = "Admin",
                DateCreated = DateTime.Now,
                SeoDecripstion = request.SeoDecripstion,
                SeoKeyword = request.SeoKeyword,
                SeoTitle = request.SeoTitle,
                DisplayOrder = request.DisplayOrder,
            };
            if (request.UploadFile != null)
            {
                var uploadAvatar = await _awsS3Service.UploadFile(request.UploadFile, _configuration["Bucket:Category"], request.Name);
                if (uploadAvatar.StatusCode == StatusCodes.Status200OK)
                {
                    category.PathImage = uploadAvatar.PresignedUrl;
                }
                else
                {
                    responseMessage.StatusCode = uploadAvatar.StatusCode;
                    responseMessage.Message = uploadAvatar.Message;
                    return responseMessage;
                }
            }
            var create = await _articleCategoryRepository.Create(category);
            if (create > 0)
            {
                responseMessage.Message = "Create Success";
                responseMessage.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                responseMessage.Message = "Create Fail";
                responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
            }
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateArticleCategory(UpdateArticleCategory request)
        {
            ResponseMessage responseMessage = new();
            var find = await _articleCategoryRepository.FindById(request.Id);
            if (find == null)
            {
                responseMessage.StatusCode = StatusCodes.Status404NotFound;
                responseMessage.Message = "No category found";
                return responseMessage;
            }

            find.Name = request.Name;
            find.Description = request.Description;
            find.DisplayOrder = request.DisplayOrder;
            find.IsDelete = request.IsDelete;
            find.SeoDecripstion = request.SeoDecripstion;
            find.SeoKeyword = request.SeoKeyword;
            find.SeoTitle = request.SeoTitle;
            find.DateUpdated = DateTime.Now;
            if (request.UploadFile != null)
            {
                var uploadAvatar = await _awsS3Service.UploadFile(request.UploadFile, _configuration["Bucket:SubCategory"], request.Name);
                if (uploadAvatar.StatusCode == StatusCodes.Status200OK)
                {
                    find.PathImage = uploadAvatar.PresignedUrl;
                }
                else
                {
                    responseMessage.StatusCode = uploadAvatar.StatusCode;
                    responseMessage.Message = uploadAvatar.Message;
                    return responseMessage;
                }
            }
            var update = await _articleCategoryRepository.Update(find);
            if (update > 0)
            {
                responseMessage.Message = "Update Success";
                responseMessage.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                responseMessage.Message = "Update Fail";
                responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
            }
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteArticleCategory(Guid id)
        {
            ResponseMessage responseMessage = new();
            var find = await _articleCategoryRepository.FindById(id);
            if (find == null)
            {
                responseMessage.StatusCode = StatusCodes.Status404NotFound;
                responseMessage.Message = "No category found";
                return responseMessage;
            }
            var hasSubCategories = await _articleSubCategoryRepository.CheckExist(id);
            if (hasSubCategories)
            {
                responseMessage.StatusCode = StatusCodes.Status400BadRequest;
                responseMessage.Message = "Cannot delete category because it has associated subcategories";
                return responseMessage;
            }

            var delete = await _articleCategoryRepository.Delete(id);
            if (delete > 0)
            {
                responseMessage.Message = "Delete Success";
                responseMessage.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                responseMessage.Message = "Delete Fail";
                responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
            }
            return responseMessage;
        }
    }
}
