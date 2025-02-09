using Article.Application.Repositories.Implements;
using Article.Application.Repositories.Interfaces;
using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Article.Data.Entity;
using Article.Dtos.ArticlePermissionDto;
using Article.Dtos.Categories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Services.Implements
{
    public class ArticlePermissionService : IArticlePermissionService
    {
        private readonly IArticlePermissionRepository _articlePermissionRepository;
        private readonly IArticleFunctionRepository _articleFunctionRepository;
        private readonly IMapper _mapper;

        public ArticlePermissionService(IArticlePermissionRepository articlePermissionRepository, IArticleFunctionRepository articleFunctionRepository, IMapper mapper)
        {
            _articlePermissionRepository = articlePermissionRepository;
            _articleFunctionRepository = articleFunctionRepository;
            _mapper = mapper;
        }

        public async Task<List<ArticlePermissionDto>> GetAll()
        {
            var permissions = _articlePermissionRepository.GetAllAsNoTracking();
            var permissionsDto = _mapper.Map<List<ArticlePermission>, List<ArticlePermissionDto>>(permissions.ToList());
            return permissionsDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">List các permission của role</param>
        /// <returns></returns>
        public async Task<ResponseMessage> CreateListArticlePermission(CreateArticlePermission request)
        {
            var responseMessage = new ResponseMessage();
            var listpermission = await _articlePermissionRepository.GetPermissionByRole(request.RoleName);

            // delete old permission 
            if (listpermission != null)
            {
                foreach (var sub in listpermission)
                {
                    await _articlePermissionRepository.Delete(sub.Id);
                }
            }
            // recreate new permission
            foreach (var content in request.ListPermission)
            {
                var permission = new ArticlePermission()
                {
                    FunctionId = content.FunctionId,
                    RoleName = content.RoleName,
                    CanCreate = content.CanCreate,
                    CanDelete = content.CanDelete,
                    CanRead = content.CanRead,
                    CanUpdate = content.CanUpdate,
                };
                var create = await _articlePermissionRepository.Create(permission);
                if (create <= 0)
                {
                    responseMessage.Message = "Create Fail";
                    responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                    return responseMessage;
                }
            }
            responseMessage.StatusCode = StatusCodes.Status200OK;
            responseMessage.Message = "Create Success";
            return responseMessage;
        }

        public async Task<ResponseMessage> UpdateContent(UpdateArticlePermission request)
        {
            var listFunction = _articleFunctionRepository.GetAll();
            var checkAll = await _articlePermissionRepository.GetPermissionByRole(request.RoleName);

            // if permission of the role > 0
            if (checkAll.Count > 0)
            {
                var find = checkAll.Where(x => x.FunctionId == request.FunctionId).FirstOrDefault();
                find.CanCreate = request.CanCreate;
                find.CanDelete = request.CanDelete;
                find.CanRead = request.CanRead;
                find.CanUpdate = request.CanUpdate;
                await _articlePermissionRepository.Update(find);
            }
            else
            {
                // If there is no permission, then create new(default is false)
                foreach (var function in listFunction)
                {
                    var permission = new ArticlePermission()
                    {
                        RoleName = request.RoleName,
                        FunctionId = function.Id,
                        CanCreate = false,
                        CanDelete = false,
                        CanRead = false,
                        CanUpdate = false
                    };
                    await _articlePermissionRepository.Create(permission);
                }
                var permssionList = _articlePermissionRepository.GetAll();
                var find = permssionList.Where(x => x.FunctionId == request.FunctionId).FirstOrDefault();
                find.CanCreate = request.CanCreate;
                find.CanDelete = request.CanDelete;
                find.CanRead = request.CanRead;
                find.CanUpdate = request.CanUpdate;
                await _articlePermissionRepository.Update(find);
            }
            return new ResponseMessage()
            {
                Message = "Success",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
