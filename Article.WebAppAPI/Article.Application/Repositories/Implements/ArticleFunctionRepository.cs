using Article.Application.Repositories.Interfaces;
using Article.Application.Repositories.RepositoryBase;
using Article.Data.EF;
using Article.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Repositories.Implements
{
    public class ArticleFunctionRepository : RepositoryBase<ArticleFunction>, IArticleFunctionRepository
    {
        public ArticleFunctionRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
