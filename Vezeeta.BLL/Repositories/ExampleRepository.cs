using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Context;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Repositories
{
    public class ExampleRepository : Repository<Example>, IExampleRepository
    {
        private readonly VezeetaDbContext _dbContext;
        private readonly DbSet<Example> _dbSet;

        public ExampleRepository(VezeetaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Example>();
        }

        public  async Task<Example> SpecialMethod()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        //implemnet custom method
    }
}
