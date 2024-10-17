﻿using System.ComponentModel.DataAnnotations;

namespace Vezeeta.PL.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        
        public string FullName => $"{FirstName} {LastName}"; // Combine First and Last Name
    }
}
