using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleCategoryDto;
using Article.Dtos.Categories;

namespace Article.Application.Services.Interfaces
{
    public interface IArticleCategoryService
    {
        Task<List<ArticleCategoryDto>> GetAll();
        Task<List<ArticleCategoryDto>> GetCategoriesWithSubCategories();
        Task<PagedList<ArticleCategoryDto>> GetPaging(PageRequest request);
        Task<ResponseMessage> CreateArticleCategory(CreateArticleCategory request);
        Task<ResponseMessage> UpdateArticleCategory(UpdateArticleCategory request);
        Task<ResponseMessage> DeleteArticleCategory(Guid id);
    }
}
