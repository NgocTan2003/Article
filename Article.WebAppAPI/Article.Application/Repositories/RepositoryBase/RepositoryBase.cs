using Article.Common.Seedwork;
using Article.Data.EF;
using AutoQueryable.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using static Article.Application.Repositories.RepositoryBase.IRepositoryBase;

namespace Article.Application.Repositories.RepositoryBase
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbSet<T> table;
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public IQueryable<T> GetAllAsNoTracking()
        {
            var getall = table.AsNoTracking();
            return getall;
        }

        public async Task<bool> Exists(Guid Id)
        {
            var find = await table.FindAsync(Id);
            if (find != null)
            {
                return true;
            }
            return false;
        }

        public async Task<T> FindById(Guid Id)
        {
            var find = await table.FindAsync(Id);
            return find;
        }

        public async Task<T> FindByIdNoTracking(Guid Id)
        {
            var et = await table.FindAsync(Id);
            if (et != null)
            {
                _context.Entry(et).State = EntityState.Detached;
            }
            return et;
        }

        public async Task<int> Create(T entity)
        {
            try
            {
                await table.AddAsync(entity);
                return await _context.SaveChangesAsync();
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
        }

        public async Task<int> Update(T entity)
        {
            try
            {
                table.Update(entity);
                return await _context.SaveChangesAsync();
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
        }

        public async Task<int> Delete(Guid Id)
        {
            try
            {
                var find = await table.FindAsync(Id);
                if (find is not null)
                {
                    table.Remove(find);
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

        public async Task<int> RemoveAll(List<Guid> request)
        {
            foreach (var sub in request)
            {
                var find = await table.FindAsync(sub);
                table.Remove(find);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedList<T>> GetPaging(PageRequest request)
        {   
            var query = GetAll();
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                var properties = typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string));
                if (properties.Any())  // Kiểm tra có thuộc tính string nào không
                {
                    var searchQuery = string.Join(" OR ", properties.Select(p => $"{p.Name}.Contains(@0)"));
                    query = query.Where(searchQuery, request.SearchText);
                }
            }

            var resultPagedList = new PagedList<T>(query, request.pageIndex, request.pageSize);
            return resultPagedList;
        }

    }
}
