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
            var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
            return View(clinics);
        }

        public async Task<IActionResult> GetAllClinics()
        {
            try
            {
                var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
                return View("~/Views/Admin/Dashboard/Clinics/Index.cshtml",clinics);
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClinic(ClinicVM clinic)
        {
            if (clinic == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {

                var newClinic = new Clinic();
                newClinic.Address = clinic.Address;
                newClinic.Name = clinic.Name;
                await _unitOfWork.Repository<Clinic>().AddAsync(newClinic);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(GetAllClinics));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the clinic.");
                }
            }
            return View("~/Views/Admin/Dashboard/Clinics/Create.cshtml", clinic);
        }


        [HttpGet]
        public IActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor( Doctor doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                 await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));   
            }
            return View(doctor);
        }
       
    }
}
