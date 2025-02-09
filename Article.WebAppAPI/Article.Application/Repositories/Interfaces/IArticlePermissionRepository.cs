using Article.Data.Entity;
using static Article.Application.Repositories.RepositoryBase.IRepositoryBase;

namespace Article.Application.Repositories.Interfaces
{
    public interface IArticlePermissionRepository : IRepositoryBase<ArticlePermission>
    {
        Task<List<ArticlePermission>> GetPermissionByRole(string roleName);
    }
}
