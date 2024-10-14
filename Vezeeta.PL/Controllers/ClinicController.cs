using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;

namespace Vezeeta.PL.Controllers
{
    //to use special method
    //var Clinic = await _unitOfWork.ClinicRepository.SpecialMethod();

    public class ClinicController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ClinicController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Clinic
        public async Task<IActionResult> Index()
        {
            var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
            return View(clinics);  
        }

        // GET: Clinic /Details/1
        public async Task<IActionResult> Details(int id)
        {
            var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);  
        }

        // GET: clinic/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }

        // POST: Clinic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Repository<Clinic>().AddAsync(clinic);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));  
            }
            return View(clinic); 
        }

        // GET: Clinic/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic); 
        }

        // POST: Clinic/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Clinic clinic)
        {
            if (id != clinic.ClinicID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                 _unitOfWork.Repository<Clinic>().UpdateAsync(clinic);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));  
            }
            return View(clinic);  
        }

        // GET: Clinic/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);  
        }

        // POST: Clinic/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clinic = _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            await _unitOfWork.Repository<Clinic>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();
             return RedirectToAction(nameof(Index)); 
        }
    }
}
