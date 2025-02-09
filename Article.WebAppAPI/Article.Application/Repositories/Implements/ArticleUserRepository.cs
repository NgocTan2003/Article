using Article.Application.Repositories.Interfaces;
using Article.Application.Repositories.RepositoryBase;
using Article.Common.Seedwork;
using Article.Data.EF;
using Article.Data.Entity;
using Article.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Repositories.Implements
{
    public class ArticleUserRepository : IArticleUserRepository
    {
        private readonly UserManager<ArticleAppUser> _userManager;

        public ArticleUserRepository(UserManager<ArticleAppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ArticleAppUser>> GetAllUser()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<PagedList<ArticleAppUser>> GetPaging(PageRequest request)
        {
            var query = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                var properties = typeof(ArticleAppUser).GetProperties().Where(p => p.PropertyType == typeof(string));
                if (properties.Any())
                {
                    var searchQuery = string.Join(" OR ", properties.Select(p => $"{p.Name}.Contains(@0)"));
                    query = query.Where(searchQuery, request.SearchText);
                }
            }
            var resultPagedList = new PagedList<ArticleAppUser>(query, request.pageIndex, request.pageSize);
            return resultPagedList;
        }

        public async Task<ArticleAppUser?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ArticleAppUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ArticleAppUser?> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IList<string>> GetAllRoleByName(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            var listRole = await _userManager.GetRolesAsync(user);
            return listRole;
        }

        public async Task<bool> AddRole(ArticleAppUser user, IList<string> Roles)
        {
            var createrole = await _userManager.AddToRolesAsync(user, Roles);
            if (createrole.Succeeded)
            {
                return true;
            }
            else
            {
                foreach (var error in createrole.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
                }
                return false;
            }
        }

        public async Task<bool> CreateUser(ArticleAppUser user, string password)
        {
            var create = await _userManager.CreateAsync(user, password);
            if (create.Succeeded)
            {
                return true;
            }
            else
            {
                foreach (var error in create.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
                }
                return false;
            }
        }

        public async Task<bool> UpdateUser(ArticleAppUser user)
        {
            var update = await _userManager.UpdateAsync(user);
            if (update.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(ArticleAppUser user)
        {
            var delete = await _userManager.DeleteAsync(user);
            if (delete.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
