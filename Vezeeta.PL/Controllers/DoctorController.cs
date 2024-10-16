using Microsoft.AspNetCore.Mvc;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;
using Vezeeta.PL.ViewModels;

namespace Vezeeta.PL.Controllers
{
    public class DoctorController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public DoctorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Clinic
        public async Task<IActionResult> Index()
        {
            try
            {
                var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
                var clinicsVM = clinics.Select(c => new ClinicVM
                {
                    ClinicID = c.ClinicID,
                    Name = c.Name,
                    Address = c.Address,
                }).ToList();

                return View(clinicsVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
