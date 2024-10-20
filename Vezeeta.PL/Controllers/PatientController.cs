using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using System.Security.Claims;
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
                 return View("~/Views/Admin/Dashboard/Patients/Index.cshtml", patientsVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // need to edit
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Doctor()
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
                return View(patientsVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
