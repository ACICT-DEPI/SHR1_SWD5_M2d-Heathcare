using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vezeeta.DAL.Entities; 
namespace Vezeeta.PL.ViewModels
{
    public class BookAppointmentVM
    {
        public List<Clinic> Clinics { get; set; } 

        [Required]
        public int ClinicId { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; } 

        [Required]
        [DataType(DataType.Time)]
        public DateTime AppointmentTime { get; set; } 
    }
}
