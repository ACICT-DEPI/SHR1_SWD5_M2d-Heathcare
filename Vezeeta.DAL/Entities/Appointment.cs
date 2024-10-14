using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Appointment
    {
        [Key]
        public int ID { get; set; }

        public string Type { get; set; }

        public string Reason { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public Doctor Doctor { get; set; }

        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }
    }

}
