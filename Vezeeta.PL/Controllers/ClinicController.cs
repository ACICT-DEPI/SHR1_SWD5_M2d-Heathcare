using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;
using System.Text.RegularExpressions;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;
using Vezeeta.PL.ViewModels;

namespace Vezeeta.PL.Controllers
{
    

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
            try { 
                var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
                var clinicsVM = clinics.Select(c => new ClinicVM
                {
                    ClinicID = c.ClinicID,
                    Name = c.Name,
                    Address = c.Address,
                }).ToList();

                return View(clinicsVM);
            }
            catch(Exception ex){
                return BadRequest();
            }
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();  
        }

        // POST: Clinic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ClinicVM clinic)
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

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) { 
                    return BadRequest();
                }
 
            }
            return View(clinic); 
        }

        // GET: Clinic/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
                if (clinic == null)
                {
                    return NotFound();
                }
                var clinicMV = new ClinicVM { Name = clinic.Name,Address =clinic.Address ,ClinicID = clinic.ClinicID };

                return View(clinicMV);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: Clinic/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClinicVM clinicMV)
        {
            if (id != clinicMV.ClinicID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingClinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
                    if (existingClinic == null)
                    {
                        return NotFound(); // Return 404 if the clinic is not found
                    }
                    existingClinic.Name = clinicMV.Name;
                    existingClinic.Address = clinicMV.Address;
                    _unitOfWork.Repository<Clinic>().UpdateAsync(existingClinic);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch(Exception ex)
                {
                    return BadRequest();
                }

            }
            return View(clinicMV);  
        }

        // GET: Clinic/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
                if (clinic == null)
                {
                    return NotFound();
                }
                var clinicMV = new ClinicVM { Name = clinic.Name, ClinicID = clinic.ClinicID };

                return View(clinicMV);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: Clinic/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id , ClinicVM clinicVM)
        {
            if(id != clinicVM.ClinicID)
            {
                return BadRequest();
            }
            try
            {
                var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
                if (clinic == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Repository<Clinic>().DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }catch(Exception ex){
                return BadRequest();
            }

        }

        public async Task<IActionResult> Search(string name)
        {
            try
            {
                if (name.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(Index));
                }

                var clinc = await _unitOfWork.Repository<Clinic>().Search(c => c.Name.ToLower().Contains(name.ToLower()));
                var clincVM = clinc.Select(c => new ClinicVM
                {
                    Name =c.Name,
                    Address = c.Address,
                    ClinicID = c.ClinicID
                }).ToList();
                return View("Index" , clincVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
     
        }

        
    }
}
