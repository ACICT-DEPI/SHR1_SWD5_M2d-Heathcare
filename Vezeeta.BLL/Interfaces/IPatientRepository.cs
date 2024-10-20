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
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> FindByUserIdAsync(string id);
    }
}
