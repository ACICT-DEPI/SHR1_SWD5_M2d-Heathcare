using System.ComponentModel.DataAnnotations;

namespace Vezeeta.PL.ViewModels
{
    public class AppointmentVM
    {
        public int ID { get; set; }

        [Required]
        public DateTime Schedule { get; set; }
        [Required]

        public string Status { get; set; }

        public string Note { get; set; }

        public string Reason { get; set; }

        public string CancellationReason { get; set; }

        public string PatientName { get; set; } 
        public string DoctorName { get; set; }  
        public string ClinicName { get; set; }  
    }
}
