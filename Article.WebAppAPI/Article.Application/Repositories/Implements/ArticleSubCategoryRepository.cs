using Article.Application.Repositories.Interfaces;
using Article.Application.Repositories.RepositoryBase;
using Article.Data.EF;
using Article.Data.Entity;

namespace Article.Application.Repositories.Implements
{
    public class ArticleSubCategoryRepository : RepositoryBase<ArticleSubCategory>, IArticleSubCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleSubCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckExist(Guid id)
        {
            var listSub =  GetAllAsNoTracking();
            var exists = listSub.Any(sub => sub.ArticleCategoryId == id);
            return exists; 
        }
    }
}
