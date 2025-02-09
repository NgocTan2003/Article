using Article.Data.Entity;
using static Article.Application.Repositories.RepositoryBase.IRepositoryBase;

namespace Article.Application.Repositories.Interfaces
{
    public interface IArticleCategoryRepository : IRepositoryBase<ArticleCategory>
    {
        Task<List<ArticleCategory>> GetCategoriesWithSubCategories();
    }
}
