using Article.Application.Repositories.Implements;
using Article.Application.Repositories.Interfaces;
using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Data.Entity;
using Article.Dtos.ArticleSubCategoryDto;
using Article.Dtos.Categories;
using Article.Dtos.SubCategories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Article.Application.Services.Implements
{
    public class ArticleSubCategoryService : IArticleSubCategoryService
    {
        private readonly IArticleSubCategoryRepository _articleSubCategoryRepository;
        private readonly IAwsS3Service _awsS3Service;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ArticleSubCategoryService(IArticleSubCategoryRepository articleSubCategoryRepository, IAwsS3Service awsS3Service, IConfiguration configuration, IMapper mapper)
        {
            _articleSubCategoryRepository = articleSubCategoryRepository;
            _awsS3Service = awsS3Service;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<ArticleSubCategoryDto>> GetAll()
        {
            var subCategories = _articleSubCategoryRepository.GetAllAsNoTracking();
            var subCategoriesDto = _mapper.Map<List<ArticleSubCategory>, List<ArticleSubCategoryDto>>(subCategories.ToList());
            return subCategoriesDto;
        }

        //public async Task<List<ArticleSubCategoryDto>> GetAllPaging(PageRequest request)
        //{
        //    var subCategories = await _articleSubCategoryRepository.GetPaging(request);
        //    var subCategoriesDto = _mapper.Map<List<ArticleSubCategory>, List<ArticleSubCategoryDto>>(subCategories);
        //    return subCategoriesDto;
        //}

        public async Task<ResponseMessage> CreateArticleSubCategory(CreateArticleSubCategory request)
        {
            ResponseMessage responseMessage = new();
            var subCategory = new ArticleSubCategory()
            {
                Name = request.Name,
                Description = request.Description,
                IsDelete = false,
                CreateBy = "Admin",
                ArticleCategoryId = request.ArticleCategoryId,
                DisplayOrder = request.DisplayOrder,
                DateCreated = DateTime.Now,
                SeoDecripstion = request.SeoDecripstion,
                SeoKeyword = request.SeoKeyword,
                SeoTitle = request.SeoTitle
            };
            if (request.UploadFile != null)
            {
                var uploadAvatar = await _awsS3Service.UploadFile(request.UploadFile, _configuration["Bucket:SubCategory"], request.Name);
                if (uploadAvatar.StatusCode == StatusCodes.Status200OK)
                {
                    subCategory.PathImage = uploadAvatar.PresignedUrl;
                }
                else
                {
                    responseMessage.StatusCode = uploadAvatar.StatusCode;
                    responseMessage.Message = uploadAvatar.Message;
                    return responseMessage;
                }
            }
            var create = await _articleSubCategoryRepository.Create(subCategory);
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

        public async Task<ResponseMessage> UpdateArticleSubCategory(UpdateArticleSubCategory request)
        {
            ResponseMessage responseMessage = new();
            var find = await _articleSubCategoryRepository.FindById(request.Id);
            if (find == null)
            {
                responseMessage.StatusCode = StatusCodes.Status404NotFound;
                responseMessage.Message = "No subCategory found";
                return responseMessage;
            }

            find.Name = request.Name;
            find.Description = request.Description;
            find.DisplayOrder = request.DisplayOrder;
            find.IsDelete = request.IsDelete;
            find.ArticleCategoryId = request.ArticleCategoryId;
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
            var update = await _articleSubCategoryRepository.Update(find);
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

        public async Task<ResponseMessage> DeleteArticleSubCategory(Guid id)
        {
            ResponseMessage responseMessage = new();
            var find = await _articleSubCategoryRepository.FindById(id);
            if (find == null)
            {
                responseMessage.StatusCode = StatusCodes.Status404NotFound;
                responseMessage.Message = "No subCategory found";
                return responseMessage;
            }
            var delete = await _articleSubCategoryRepository.Delete(id);
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
