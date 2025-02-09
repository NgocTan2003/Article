using Article.Application.Repositories.Interfaces;
using Article.Application.Repositories.RepositoryBase;
using Article.Data.EF;
using Article.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Repositories.Implements
{
    public class ArticleCategoryRepository : RepositoryBase<ArticleCategory>, IArticleCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ArticleCategory>> GetCategoriesWithSubCategories()
        {
            var getAll = await _context.ArticleCategories.Include(x => x.ArticleSubCategories).Where(x => !x.IsDelete).ToListAsync();
            return getAll;
        }
    }
}
