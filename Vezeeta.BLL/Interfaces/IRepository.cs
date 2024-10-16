using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.BLL.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<IEnumerable<T>> Search(Expression<Func<T , bool>> criteria);
    }
}
