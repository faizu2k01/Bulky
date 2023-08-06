using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bulky.DataAccess.Repository
{
    public class RepositroyImp<T> : IRepository<T> where T : class
    {
        public readonly DataContext _dataContext;

        internal DbSet<T> _dbSet;
        public RepositroyImp(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }


        public async Task<bool> AddAsync(T enityt)
        {
            await _dbSet.AddAsync(enityt);
            return await _dataContext.SaveChangesAsync() > 0;
        }


       
        public async Task<T> Get(Expression<Func<T, bool>> expression,string? includeProperties=null)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
            foreach(var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                   query = query.Include(item);
                }

            }
            return await query.Where(expression).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> GetAll(string? includeProperties=null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                   query = query.Include(item);
                }

            }
            return await query.ToListAsync();
        }

        public async Task<bool> Remove(T entity)
        {
            _dbSet.Remove(entity);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRange(T entity)
        {
            _dbSet.RemoveRange(entity);
            return await _dataContext.SaveChangesAsync()>0;
        }
    }
}
