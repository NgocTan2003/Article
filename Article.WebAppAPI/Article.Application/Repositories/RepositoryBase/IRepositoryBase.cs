using Article.Common.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Repositories.RepositoryBase
{
    public interface IRepositoryBase
    {
        public interface IRepositoryBase<T> where T : class
        {
            IQueryable<T> GetAll();
            IQueryable<T> GetAllAsNoTracking();
            Task<bool> Exists(Guid Id);
            Task<T> FindById(Guid Id);
            Task<T> FindByIdNoTracking(Guid Id);
            Task<int> Create(T entity);
            Task<int> Update(T entity);
            Task<int> Delete(Guid Id);
           // Task<List<T>> GetAllPaging(PageRequest request);
            Task<PagedList<T>> GetPaging(PageRequest request);
            Task<int> RemoveAll(List<Guid> request);
        }
    }
}
