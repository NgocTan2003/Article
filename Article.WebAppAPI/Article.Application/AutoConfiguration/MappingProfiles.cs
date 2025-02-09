using Article.Data.Entity;
using Article.Dtos.Categories;
using Article.Dtos.Datas;
using Article.Dtos.Roles;
using Article.Dtos.SubCategories;
using Article.Dtos.Tags;
using Article.Dtos.UserDto;
using Article.Dtos.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.AutoConfiguration
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ArticleAppUser, ArticleUserDto>().ReverseMap();
            CreateMap<ArticleAppUser, UserDto>().ReverseMap();
            CreateMap<ArticleCategory, ArticleCategoryDto>().ReverseMap();
            CreateMap<ArticleAppRole, ArticleRoleDto>().ReverseMap();
            CreateMap<ArticleData, ArticleDataDto>().ReverseMap();
            CreateMap<ArticleSubCategory, ArticleSubCategoryDto>().ReverseMap();
            CreateMap<ArticleTag, ArticleTagDto>().ReverseMap();

        }
    }
}
