using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Patient
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public string Username { get; set; }

        public string NationalID { get; set; }

        public bool ChronicDisease { get; set; }

        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public Location Location { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();
    }

}
