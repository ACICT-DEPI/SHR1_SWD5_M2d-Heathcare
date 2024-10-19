using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Vezeeta.PL.ViewModels
{
    public class DoctorVM
    {
            public int DoctorID { get; set; }

            [Required(ErrorMessage = "First name is required")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last name is required")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Phone number is required")]
            [Phone(ErrorMessage = "Invalid phone number")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Clinic ID is required")]
            public int ClinicID { get; set; }

            public string? ClinicName { get; set; }
             public string? ClinicAddress { get; set; }

    }
}
