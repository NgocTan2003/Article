using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.Categories;
using Article.Dtos.Roles;

namespace Article.WebApp.ConnectAPI.ArticleCategoryCnAPI
{
    public interface IArticleCategoryConnectAPI
    {
        Task<ResponsePageListItem<ArticleCategoryDto>> GetPaging(PageRequest request);

    }
}
