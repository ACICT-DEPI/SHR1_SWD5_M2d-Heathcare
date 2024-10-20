using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.BLL.DTOs;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<IEnumerable<DoctorWithClinicDto>> GetDoctorsWithClinicsAsync();
        Task<IEnumerable<DoctorWithClinicDto>> CustomSearch(Expression<Func<Doctor, bool>> criteria);

        Task<Doctor> FindByUserIdAsync(string id);

    }
}
