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
    class DoctorRepository : Repository<Doctor> , IDoctorRepository
    {
       
        private readonly VezeetaDbContext _dbContext;
        private readonly DbSet<Doctor> _dbSet;
        public DoctorRepository(VezeetaDbContext dbContext ) :base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Doctor>();
        }

        public async Task<IEnumerable<DoctorWithClinicDto>> GetDoctorsWithClinicsAsync()
        {
           return await _dbSet.Include(d => d.Clinic).Select(d  => new DoctorWithClinicDto()
           {
               DoctorID = d.DoctorID,
               FirstName = d.FirstName,
               LastName = d.LastName,
               Email = d.Email,
               Phone = d.Phone,
               ClinicID = d.ClinicID,
               ClinicName = d.Clinic.Name,
               ClinicAddress = d.Clinic.Address,
           }).ToListAsync();
        }
         public async Task<IEnumerable<DoctorWithClinicDto>> CustomSearch(Expression<Func<Doctor, bool>> criteria)
        {
            return await _dbSet.Where(criteria).Include(d => d.Clinic).Select(d => new DoctorWithClinicDto()
            {
                DoctorID = d.DoctorID,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Phone = d.Phone,
                ClinicID = d.ClinicID,
                ClinicName = d.Clinic.Name,
                ClinicAddress = d.Clinic.Address,
            }).ToListAsync();
        }
        public async Task<Doctor> FindByUserIdAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.ApplicationUserId == id);
        }
    }
}
