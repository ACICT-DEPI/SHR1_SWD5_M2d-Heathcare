using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Phone
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public int EntityID { get; set; } //  DoctorID or PatientID

        public string EntityType { get; set; } // To identify whether it's for a Patient or a Doctor

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }

}
