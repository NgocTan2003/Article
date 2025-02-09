using Article.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Article.Application.Repositories.RepositoryBase.IRepositoryBase;

namespace Article.Application.Repositories.Interfaces
{
    public interface IArticleFunctionRepository : IRepositoryBase<ArticleFunction>
    {
        
    }
}
