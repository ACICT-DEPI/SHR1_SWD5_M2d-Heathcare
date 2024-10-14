using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;

        IExampleRepository ExampleRepository { get; }

        Task<int> SaveAsync();
    }
}
