using Article.Dtos.ArticleRoleDto;
using Article.Dtos.Roles;
using AutoMapper;

namespace Article.WebApp.AutoConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArticleRoleDto, UpdateArticleRole>();
        }
    }
}
