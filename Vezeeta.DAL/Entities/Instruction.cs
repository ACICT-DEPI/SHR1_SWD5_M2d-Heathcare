using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Instruction
    {
        [Key]
        public int ID { get; set; }

        public string Dose { get; set; }

        public string Frequency { get; set; } // Frequency of dosage

        public string Duration { get; set; }

        public int PrescriptionID { get; set; }

        [ForeignKey("PrescriptionID")]
        public Prescription Prescription { get; set; }
    }

}
