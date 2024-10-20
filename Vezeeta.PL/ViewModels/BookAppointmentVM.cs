using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Vezeeta.DAL.Entities;

namespace Vezeeta.PL.ViewModels
{
    public class BookAppointmentVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Schedule is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date and time format.")]
        public DateTime Schedule { get; set; }


        [StringLength(250, ErrorMessage = "Note cannot be longer than 250 characters.")]
        public string Note { get; set; }

        [StringLength(250, ErrorMessage = "Reason cannot be longer than 250 characters.")]
        public string Reason { get; set; }

        public string SelectedDoctorId { get; set; }
        public List<SelectListItem>? DoctorsSelectList { get; set; }
    }
}
