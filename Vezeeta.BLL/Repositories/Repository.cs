using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Context;

namespace Vezeeta.BLL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VezeetaDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(VezeetaDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> criteria)
        {
            return await _dbSet.Where(criteria).ToListAsync();
        }
    }
}
