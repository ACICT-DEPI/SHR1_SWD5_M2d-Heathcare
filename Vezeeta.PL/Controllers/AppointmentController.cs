using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;
using Vezeeta.PL.ViewModels;

namespace Vezeeta.PL.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Appointments 
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var appointmnets = await _unitOfWork.AppointmentRepository.GetAppointmentsWithRelatedTablesAsync();
                var appointmnetsVM = appointmnets.Select(d => new AppointmentVM
                {
                    ID = d.ID,
                    Status = d.Status,
                    Schedule = d.Schedule,
                    Note = d.Note,  
                    Reason = d.Reason,
                    CancellationReason = d.CancellationReason,
                    PatientName = d.PatientName,
                    DoctorName = d.DoctorName,
                    ClinicName = d.ClinicName,
                }).ToList();

                if (User.IsInRole("Admin"))
                    return View("~/Views/Admin/Dashboard/Appointments/Index.cshtml", appointmnetsVM);
                else if (User.IsInRole("Doctor") || User.IsInRole("Patient"))
                    return View(appointmnetsVM);
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
