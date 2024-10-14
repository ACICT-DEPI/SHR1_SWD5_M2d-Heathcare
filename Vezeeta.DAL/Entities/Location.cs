using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Location
    {
        [Key]
        public int ID { get; set; }

        public string City { get; set; }
        public string StreetName { get; set; }
        public string BuildingNumber { get; set; }
        public int Floor { get; set; }
        public string Government { get; set; }
        public Clinic Clinic { get; set; }
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }

}
