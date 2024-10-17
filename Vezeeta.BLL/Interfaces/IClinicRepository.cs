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
        
        public  Task<Clinic> SpecialMethod();
        Task<List<Clinic>> GetAllAsync(); 
        

    }
}
