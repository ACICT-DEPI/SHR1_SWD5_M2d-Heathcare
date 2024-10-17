namespace Vezeeta.PL.ViewModels
{
    public class PatientDashboardVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<ClinicVM> Clinics { get; set; }
    }
}
