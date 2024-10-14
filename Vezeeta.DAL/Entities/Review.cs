using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Review
    {
        [Key]
        public int ID { get; set; }

        public int Rating { get; set; } 

        public string Comment { get; set; }

        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public Doctor Doctor { get; set; }

        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }

        public DateTime Date { get; set; }
    }

}
