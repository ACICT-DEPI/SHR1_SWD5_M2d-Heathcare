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

        public DateTime Schedule { get; set; }


        public string Status { get; set; }

        public string Note { get; set; }

        public string Reason { get; set; }

        public string CancellationReason { get; set; }

        // Relationships


        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }
    }

}
