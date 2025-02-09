using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.ArticleRoleDto;
using Article.Dtos.Categories;
using Article.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Services.Interfaces
{
    public interface IArticleRoleService
    {
        Task<List<ArticleRoleDto>> GetAll();
        Task<PagedList<ArticleRoleDto>> GetPaging(PageRequest request);
        Task<ArticleRoleDto> FindRoleById(string Id);
        Task<ResponseMessage> CreateRole(CreateArticleRole request);
        Task<ResponseMessage> UpdateRole(UpdateArticleRole request);
        Task<ResponseMessage> DeleteRole(string Id);
    }
}
