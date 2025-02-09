using Article.Application.Repositories.Interfaces;
using Article.Application.Repositories.RepositoryBase;
using Article.Data.EF;
using Article.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Repositories.Implements
{
    public class ArticleRoleRepository : RepositoryBase<ArticleAppRole>, IArticleRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckExistName(string name)
        {
            var listRole = GetAllAsNoTracking();
            var exists = listRole.Any(sub => sub.Name == name);
            return exists;
        }

        public async Task<ArticleAppRole> FindRoleById(string Id)
        {
            var role = await _context.Roles.FindAsync(Id);
            return role;
        }

        public async Task<int> Delete(string Id)
        {
            try
            {
                var find = await FindRoleById(Id);
                if (find is not null)
                {
                    _context.Roles.Remove(find);
                    return await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Update Error: {dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            return 0;
        }
    }
}
