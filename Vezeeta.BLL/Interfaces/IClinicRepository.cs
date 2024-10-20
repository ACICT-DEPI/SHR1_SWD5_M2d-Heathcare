using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Interfaces
{
    public interface IClinicRepository : IRepository<Clinic>
    {
        //add any custome method for this entity 
        Task<List<Clinic>> GetAllWithDoctorsAsync();
        Task<Clinic> SpecialMethod();

    }
}
