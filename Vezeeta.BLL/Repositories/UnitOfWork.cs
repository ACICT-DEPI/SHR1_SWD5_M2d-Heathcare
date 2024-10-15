using System;
using System.Threading.Tasks;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Vezeeta.BLL.Repositories;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VezeetaDbContext _dbContext;
        private bool _disposed;
        private readonly Dictionary<Type, object> _repositories = new();

        public IClinicRepository ClinicRepository { get; private set; }

        public UnitOfWork(VezeetaDbContext dbContext)
        {
            _dbContext = dbContext;
            ClinicRepository = new ClinicRepository(dbContext);
        }



        // Get or create repository for a specific type
        public IRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new Repository<T>(_dbContext);  
            }

            return (IRepository<T>)_repositories[typeof(T)];
        }






        // Save changes asynchronously
        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        // Dispose method to release resources
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
