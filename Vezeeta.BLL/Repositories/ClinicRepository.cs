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
    public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
        private readonly VezeetaDbContext _dbContext;
        private readonly DbSet<Clinic> _dbSet;

        public ClinicRepository(VezeetaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Clinic>();
        }

        public async Task<Clinic> SpecialMethod()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        //implemnet custom method
        public async Task<List<Clinic>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // Retrieves all clinics
        }
    }
}
