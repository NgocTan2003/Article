using Article.Application.Repositories.Implements;
using Article.Application.Repositories.Interfaces;
using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Data.Entity;
using Article.Dtos.ArticleRoleDto;
using Article.Dtos.Categories;
using Article.Dtos.Roles;
using Article.Dtos.SubCategories;
using Article.Dtos.UserDto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Article.Application.Services.Implements
{
    public class ArticleRoleService : IArticleRoleService
    {
        private readonly IArticleRoleRepository _articleRoleRepository;
        private readonly IMapper _mapper;

        public ArticleRoleService(IArticleRoleRepository articleRoleRepository, IMapper mapper)
        {
            _articleRoleRepository = articleRoleRepository;
            _mapper = mapper;
        }

        public async Task<List<ArticleRoleDto>> GetAll()
        {
            var roles = _articleRoleRepository.GetAllAsNoTracking();
            var rolesDto = _mapper.Map<List<ArticleAppRole>, List<ArticleRoleDto>>(roles.ToList());
            return rolesDto;
        }

        public async Task<PagedList<ArticleRoleDto>> GetPaging(PageRequest request)
        {
            var pagedList = await _articleRoleRepository.GetPaging(request);
            var mappedItems = _mapper.Map<List<ArticleRoleDto>>(pagedList.TotalItems);
            return new PagedList<ArticleRoleDto>(mappedItems, pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalPages, pagedList.StartPage, pagedList.EndPage);
        }

        public async Task<ArticleRoleDto> FindRoleById(string Id)
        {
            var role = await _articleRoleRepository.FindRoleById(Id);
            var roleDto = _mapper.Map<ArticleRoleDto>(role);
            return roleDto;
        }

        public async Task<ResponseMessage> CreateRole(CreateArticleRole request)
        {
            ResponseMessage responseMessage = new();
            var checkName = await _articleRoleRepository.CheckExistName(request.Name);
            if (checkName)
            {
                responseMessage.StatusCode = StatusCodes.Status409Conflict;
                responseMessage.Message = "Name Role Exist";
                return responseMessage;
            }

            var role = new ArticleAppRole()
            {
                Name = request.Name,
                Description = request.Description,
                CreateBy = "Admin",
                DateCreated = DateTime.Now,
                SeoKeyword = request.SeoKeyword,
                SeoDecripstion = request.SeoDecripstion,
                SeoTitle = request.SeoTitle
            };

            var create = await _articleRoleRepository.Create(role);
            if (create > 0)
            {
                responseMessage.StatusCode = StatusCodes.Status200OK;
                responseMessage.Message = "Create Success";
            }
            else
            {
                responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                responseMessage.Message = "Create Fail";
            }
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateRole(UpdateArticleRole request)
        {
            ResponseMessage responseMessage = new();
            var find = await _articleRoleRepository.FindRoleById(request.Id);
            if (find != null)
            {
                responseMessage.StatusCode = StatusCodes.Status404NotFound;
                responseMessage.Message = "Not Found";
            }

            find.Name = request.Name;
            find.Description = request.Description;
            find.DateUpdated = DateTime.Now;
            find.SeoDecripstion = request.SeoDecripstion;
            find.SeoKeyword = request.SeoKeyword;
            find.SeoTitle = request.SeoTitle;

            var update = await _articleRoleRepository.Update(find);
            if (update > 0)
            {
                responseMessage.StatusCode = StatusCodes.Status200OK;
                responseMessage.Message = "Update Success";
            }
            else
            {
                responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                responseMessage.Message = "Update Fail";
            }
            return responseMessage;
        }

        public async Task<ResponseMessage> DeleteRole(string Id)
        {
            ResponseMessage responseMessage = new();
            var find = await _articleRoleRepository.FindRoleById(Id);
            if (find == null)
            {
                responseMessage.StatusCode = StatusCodes.Status404NotFound;
                responseMessage.Message = "Not Found";
                return responseMessage;
            }

            var delete = await _articleRoleRepository.Delete(Id);
            if (delete > 0)
            {
                responseMessage.StatusCode = StatusCodes.Status200OK;
                responseMessage.Message = "Delete Success";
            }
            else
            {
                responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                responseMessage.Message = "Delete Fail";
            }
            return responseMessage;
        }
    }
}
