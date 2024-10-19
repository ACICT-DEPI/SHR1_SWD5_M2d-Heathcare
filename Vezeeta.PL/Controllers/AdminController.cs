using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;
using Vezeeta.PL.ViewModels;

namespace Vezeeta.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork) { 
        _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var AppointmentsGroupedByStatus = await _unitOfWork.AppointmentRepository.GetGetAppointmentsGroupedByStatusAsync();
            var adminVM = AppointmentsGroupedByStatus.Select(a => new AdminVM
            {
                Status = a.Status,
                Count = a.Count
            }).ToList();
            return View(adminVM);
        }
    }
}
