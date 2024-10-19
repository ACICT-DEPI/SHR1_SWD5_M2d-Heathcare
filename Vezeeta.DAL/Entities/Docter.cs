using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Vezeeta.DAL.Entities
{
    public class Doctor
    {

            [Key]
            public int DoctorID { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }

            // Relationships
            public int ClinicID { get; set; }
            public Clinic Clinic { get; set; }

            public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
            
            public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();


            // With ApplicationUser
            public string ApplicationUserId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
    }


}
