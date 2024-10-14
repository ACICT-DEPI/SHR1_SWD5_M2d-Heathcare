using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Clinic
    {
        [Key]
        public int ClinicID { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        // Relationships
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }

}
