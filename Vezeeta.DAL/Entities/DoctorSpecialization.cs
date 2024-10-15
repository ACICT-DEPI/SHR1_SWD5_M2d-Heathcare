using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class DoctorSpecialization
    {

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public int SpecializationID { get; set; }
        public Specialization Specialization { get; set; }
    }


}
