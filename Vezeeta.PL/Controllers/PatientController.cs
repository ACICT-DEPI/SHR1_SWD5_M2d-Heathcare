using Microsoft.AspNetCore.Mvc;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;
using Vezeeta.PL.ViewModels;

namespace Vezeeta.PL.Controllers
{
    public class PatientController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var patients = await _unitOfWork.Repository<Patient>().GetAllAsync();
                var patientsVM = patients.Select(patient => new PatientVM
                {
                    PatientID = patient.PatientID,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    Phone = patient.Phone,
                    DateOfBirth = patient.DateOfBirth,
                    AppointmentCount = patient.Appointments.Count()
                }).ToList();


                if (User.IsInRole("Admin"))
                    return View("~/Views/Admin/Dashboard/Patients/Index.cshtml", patientsVM);
                else if (User.IsInRole("Doctor") || User.IsInRole("Patient"))
                    return View(patientsVM);
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
