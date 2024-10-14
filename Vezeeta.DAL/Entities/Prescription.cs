using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Prescription
    {
        [Key]
        public int ID { get; set; }

        public string Medication { get; set; }

        public string Dosage { get; set; }


        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }

        public DateTime DateIssued { get; set; }

        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
    }

}
