using Article.Application.Repositories.RepositoryBase;
using Article.Data.EF;
using Article.Data.Entity;
using Article.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Article.Application.Repositories.RepositoryBase.IRepositoryBase;

namespace Article.Application.Repositories.Interfaces
{
    public interface IArticleRoleRepository: IRepositoryBase<ArticleAppRole>
    {
        Task<bool> CheckExistName(string name);
        Task<ArticleAppRole> FindRoleById(string Id);
        Task<int> Delete(string Id);
    }
}
