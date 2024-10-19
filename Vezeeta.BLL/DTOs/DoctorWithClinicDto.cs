using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.BLL.DTOs
{
    public class DoctorWithClinicDto
    {
        public int DoctorID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
    }
}
