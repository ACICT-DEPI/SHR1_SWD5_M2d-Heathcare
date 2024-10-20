using System.ComponentModel.DataAnnotations;

namespace Vezeeta.PL.ViewModels
{
    public class CancelAppointmentVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Cancellation Reason is required.")]
        [StringLength(250, ErrorMessage = "Cancellation Reason cannot be longer than 250 characters.")]
        public string CancellationReason { get; set; }
    }
}
