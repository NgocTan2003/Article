using Article.Common.ReponseBase;
using Article.Dtos.ArticlePermissionDto;

namespace Article.Application.Services.Interfaces
{
    public interface IArticlePermissionService
    {
        Task<List<ArticlePermissionDto>> GetAll();
        Task<ResponseMessage> CreateListArticlePermission(CreateArticlePermission request);
        Task<ResponseMessage> UpdateContent(UpdateArticlePermission request);
    }
}
