using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Interfaces
{
    public interface IExampleRepository : IRepository<Example>
    {
        //add any custome method for this entity 
        public  Task<Example> SpecialMethod();

    }
}
