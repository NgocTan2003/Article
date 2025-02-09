using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleCategoryDto;
using Article.Dtos.ArticleSubCategoryDto;
using Article.Dtos.SubCategories;

namespace Article.Application.Services.Interfaces
{
    public interface IArticleSubCategoryService
    {
        Task<List<ArticleSubCategoryDto>> GetAll();
        //Task<List<ArticleSubCategoryDto>> GetAllPaging(PageRequest request);
        Task<ResponseMessage> CreateArticleSubCategory(CreateArticleSubCategory request);
        Task<ResponseMessage> UpdateArticleSubCategory(UpdateArticleSubCategory request);
        Task<ResponseMessage> DeleteArticleSubCategory(Guid id);
    }
}
