using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.BLL.DTOs;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Context;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Repositories
{
    public class PatientRepository : Repository<Patient> , IPatientRepository
    {
        private readonly VezeetaDbContext _dbContext;
        private readonly DbSet<Patient> _dbSet;
        public PatientRepository(VezeetaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Patient>();
        }

        //add custom method implementation

        public async Task<Patient> FindByUserIdAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.ApplicationUserId == id);
        }

    }
}
