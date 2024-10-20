using System.ComponentModel.DataAnnotations;

namespace Vezeeta.PL.ViewModels
{
    public class ScheduleAppointmentVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Schedule is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date and time format.")]
        public DateTime Schedule { get; set; }


        [StringLength(250, ErrorMessage = "Note cannot be longer than 250 characters.")]
        public string Note { get; set; }
    }
}
