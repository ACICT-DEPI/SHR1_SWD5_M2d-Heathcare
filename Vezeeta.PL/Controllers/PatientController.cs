using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;
using Vezeeta.PL.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Vezeeta.PL.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager; 

  
        public PatientController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        // GET: Patient Dashboard
        public async Task<IActionResult> Index()
        {
            try
            {
                // Get the currently logged-in patient
                var user = await _userManager.GetUserAsync(User);

                // Fetch clinics and map them to ClinicVM
                var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
                var clinicsVM = clinics.Select(c => new ClinicVM
                {
                    ClinicID = c.ClinicID,
                    Name = c.Name,
                    Address = c.Address,
                }).ToList();

                // Create a view model that includes the patient's FullName, Email, and Clinics
                var patientDashboardVM = new PatientDashboardVM
                {
                    FullName = user.FullName, // Patient's full name
                    Email = user.Email,       // Patient's email
                    Clinics = clinicsVM       // List of clinics
                };

                return View(patientDashboardVM);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return BadRequest(ex.Message); // Return the error message for debugging
            }
        }

        // GET: Book an Appointment
        public async Task<IActionResult> BookAppointment()
        {
            var clinics = await _unitOfWork.ClinicRepository.GetAllAsync(); 

            var model = new BookAppointmentVM
            {
                Clinics = clinics 
            };

            return View(model);
        }

        // POST: Book an Appointment
        [HttpPost]
        public async Task<IActionResult> BookAppointment(BookAppointmentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Clinics = await _unitOfWork.ClinicRepository.GetAllAsync(); // Repopulate clinics
                return View(model);
            }

            // Combine date and time for the appointment
            var appointmentTime = model.AppointmentDate.Add(model.AppointmentTime.TimeOfDay);

            // Check if there's already an appointment at this time
            var existingAppointment = await _unitOfWork.Repository<Appointment>()
                .GetAllAsync(a => a.ClinicID == model.ClinicId && a.Schedule == appointmentTime);

            if (existingAppointment.Any()) // Use .Any() to check if there are any existing appointments
            {
                ModelState.AddModelError(string.Empty, "This appointment slot is already taken. Please choose a different time.");
                model.Clinics = await _unitOfWork.ClinicRepository.GetAllAsync(); // Repopulate clinics
                return View(model);
            }

            // Create and save the new appointment
            var user = await _userManager.GetUserAsync(User); // Get the currently logged-in user

            var appointment = new Appointment
            {
                Schedule = appointmentTime,
                ClinicID = model.ClinicId,
                Patient = new Patient
                {
                    FirstName = user.FullName?.Split(' ').FirstOrDefault(), // Get the first part of the FullName
                    LastName = user.FullName?.Split(' ').Skip(1).FirstOrDefault(),    // Use the properties from ApplicationUser
                    Email = user.Email,           // Assuming you want to save the email
                    Phone = user.PhoneNumber,     // Assuming you want to save the phone number
                },
                Status = "Scheduled",
            };

            await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("ViewAppointments");
        }

        // GET: View Appointments
        public IActionResult ViewAppointments()
        {
            return View();
        }

        // POST: Logout Action
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account"); 
        }
    }
}
