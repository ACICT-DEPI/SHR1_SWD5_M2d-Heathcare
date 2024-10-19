using System.ComponentModel.DataAnnotations;

namespace Vezeeta.PL.ViewModels
{
    public class AppointmentVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Schedule is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date and time format.")]
        public DateTime Schedule { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters.")]
        [RegularExpression("scheduled|pending|cancelled", ErrorMessage = "Status must be either 'Scheduled', 'Pending', or 'Cancelled'.")]
        public string Status { get; set; }

        [StringLength(250, ErrorMessage = "Note cannot be longer than 250 characters.")]
        public string Note { get; set; }

        [StringLength(250, ErrorMessage = "Reason cannot be longer than 250 characters.")]
        public string Reason { get; set; }

        [StringLength(250, ErrorMessage = "Cancellation Reason cannot be longer than 250 characters.")]
        public string? CancellationReason { get; set; }

        [Required(ErrorMessage = "Patient name is required.")]
        [StringLength(100, ErrorMessage = "Patient name cannot be longer than 100 characters.")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Doctor name is required.")]
        [StringLength(100, ErrorMessage = "Doctor name cannot be longer than 100 characters.")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Clinic name is required.")]
        [StringLength(100, ErrorMessage = "Clinic name cannot be longer than 100 characters.")]
        public string ClinicName { get; set; }
    }
}
