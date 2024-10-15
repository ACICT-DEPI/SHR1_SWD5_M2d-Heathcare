using System.ComponentModel.DataAnnotations;

namespace Vezeeta.PL.ViewModels
{

        public class ClinicVM
        {
            public int ClinicID { get; set; }

            [Required(ErrorMessage = "Clinic name is required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Address is required")]
            public string Address { get; set; }
        }

    
}
