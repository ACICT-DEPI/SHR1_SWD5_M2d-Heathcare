using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Allergy
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string AllergyName { get; set; }

        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }
    }

}
