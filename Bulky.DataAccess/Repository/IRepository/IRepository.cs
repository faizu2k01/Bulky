using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<IEnumerable<T>> GetAll(string? includeProperties=null);
        Task<T> Get(Expression<Func<T, bool>> expression,string? includeProperties=null);
       
        Task<bool> Remove(T entity);
        Task<bool> RemoveRange(T entity);
    }
}
