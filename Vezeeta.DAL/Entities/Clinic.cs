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
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public int LocationId { get; set; }
        public Location Location { get; set; }

    }

}
