using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleRoleDto;
using Article.Dtos.Categories;
using Article.Dtos.Roles;

namespace Article.WebApp.ConnectAPI.ArticleRoleCnAPI
{
    public interface IArticleAppRoleConnectAPI
    {
        Task<List<ArticleRoleDto>> GetAll();
        Task<ResponsePageListItem<ArticleRoleDto>> GetPaging(PageRequest request);
        Task<ArticleRoleDto> FindRoleById(string Id);
        Task<ResponseMessage> CreateRole(CreateArticleRole request);
        Task<ResponseMessage> UpdateRole(UpdateArticleRole request);
        Task<ResponseMessage> DeleteRole(string Id);

    }
}
