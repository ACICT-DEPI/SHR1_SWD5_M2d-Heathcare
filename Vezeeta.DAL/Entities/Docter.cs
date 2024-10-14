using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Doctor
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string NationalID { get; set; }

        public bool ChronicDisease { get; set; }

        public int ClinicID { get; set; }

        [ForeignKey("ClinicID")]
        public Clinic Clinic { get; set; }

        public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();


        public int SpecializationId { get; set; }

        [ForeignKey("SpecializationId")]
        public Specialization Specialization { get; set; }
    }

}
