using Article.Common.Seedwork;
using Article.Data.Entity;

namespace Article.Application.Repositories.Interfaces
{
    public interface IArticleUserRepository
    {
        Task<List<ArticleAppUser>> GetAllUser();
        Task<PagedList<ArticleAppUser>> GetPaging(PageRequest request);
        Task<ArticleAppUser> GetUserById(string id);
        Task<ArticleAppUser?> GetUserByUsername(string username);
        Task<ArticleAppUser?> GetUserByEmail(string email);
        Task<bool> AddRole(ArticleAppUser user, IList<string> Roles);
        Task<IList<string>> GetAllRoleByName(string UserName);
        Task<bool> CreateUser(ArticleAppUser user, string password);
        Task<bool> UpdateUser(ArticleAppUser user);
        Task<bool> DeleteUser(ArticleAppUser user);
    }
}
