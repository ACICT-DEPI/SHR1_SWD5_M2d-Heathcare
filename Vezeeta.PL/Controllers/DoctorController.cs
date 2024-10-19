using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Entities;
using Vezeeta.PL.ViewModels;

namespace Vezeeta.PL.Controllers
{
    public class DoctorController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorController(IUnitOfWork unitOfWork , UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private async Task<List<SelectListItem>> GetClinicsAsync()
        {
            var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
            return clinics.Select(c => new SelectListItem
            {
                Value = c.ClinicID.ToString(),
                Text = c.Name
            }).ToList();
        }


        // GET: Doctors with clinic details
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var doctors = await _unitOfWork.DoctorRepository.GetDoctorsWithClinicsAsync();
                var doctorsVM = doctors.Select(d => new DoctorVM
                {
                    DoctorID = d.DoctorID,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Email = d.Email,
                    Phone = d.Phone,
                    ClinicID = d.ClinicID,
                    ClinicName = d.ClinicName,
                    ClinicAddress = d.ClinicAddress,
                }).ToList();

                if (User.IsInRole("Admin"))
                    return View("~/Views/Admin/Dashboard/Doctors/Index.cshtml", doctorsVM);
                else if (User.IsInRole("Doctor") || User.IsInRole("Patient"))
                    return View(doctorsVM);
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // GET: Doctor /Details/1
        public async Task<IActionResult> Details(int id)
        {
            var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }



        // GET: Doctor/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var clinics = await GetClinicsAsync();
            ViewBag.Clinics = clinics;
            return View("~/Views/Admin/Dashboard/Doctors/Create.cshtml");
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateDoctorVM doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
    
                    var user = new ApplicationUser()
                    {
                        UserName = doctor.Email,
                        Email = doctor.Email,
                        PhoneNumber = doctor.Phone,
                        FullName = doctor.FirstName + " " + doctor.LastName
                    };
                    var result = await _userManager.CreateAsync(user, doctor.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Doctor");

                        var newDoctor = new Doctor()
                        {
                            FirstName = doctor.FirstName,
                            LastName = doctor.LastName,
                            Email = doctor.Email,
                            Phone = doctor.Phone,
                            ClinicID = doctor.ClinicID,
                            ApplicationUserId = user.Id,
                        };

                        await _unitOfWork.Repository<Doctor>().AddAsync(newDoctor);
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
            var clinics = await GetClinicsAsync();
            ViewBag.Clinics = clinics;
            return View("~/Views/Admin/Dashboard/Doctors/Create.cshtml", doctor);
        }

        // GET: Doctor/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
                if (doctor == null)
                    return NotFound();
                var clinics = await GetClinicsAsync();
                ViewBag.Clinics = clinics;
                var doctorMV = new DoctorVM {DoctorID = doctor.DoctorID, FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email , Phone = doctor.Phone  , ClinicID = doctor.ClinicID };
                return View("~/Views/Admin/Dashboard/Doctors/Edit.cshtml", doctorMV);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorVM doctorMV)
        {
            if (id != doctorMV.DoctorID)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                await GetClinicsAsync();
                return View("~/Views/Admin/Dashboard/Doctors/Edit.cshtml", doctorMV);
            }
            try
            {
                var existingDoctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
                if (existingDoctor == null)
                {
                    return NotFound(); 
                }
                existingDoctor.FirstName = doctorMV.FirstName;
                existingDoctor.LastName = doctorMV.LastName;
                existingDoctor.Email = doctorMV.Email;
                existingDoctor.Phone = doctorMV.Phone;
                existingDoctor.ClinicID = doctorMV.ClinicID;

                _unitOfWork.Repository<Doctor>().UpdateAsync(existingDoctor);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                TempData["ErrorMessage"] = "Unable to delete this clinic because it has related records (e.g., doctors or appointments). Please remove related data before attempting to delete.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // GET: Doctor/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
                if (doctor == null)
                {
                    return NotFound();
                }
                var doctorMV = new DoctorVM { FirstName = doctor.FirstName,LastName = doctor.LastName, DoctorID = doctor.DoctorID };

                return View("~/Views/Admin/Dashboard/Doctors/Delete.cshtml", doctorMV);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: Doctor/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DoctorVM doctorMV)
        {
            if (id != doctorMV.DoctorID)
            {
                return BadRequest();
            }
            try
            {
                var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
                if (doctor == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Repository<Doctor>().DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
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

                var doctors = await _unitOfWork.DoctorRepository.CustomSearch(d => d.FirstName.ToLower().Contains(name.ToLower()));
                var doctorsVM = doctors.Select(d => new DoctorVM
                {
                    DoctorID = d.DoctorID,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Email = d.Email,
                    Phone = d.Phone,
                    ClinicID = d.ClinicID,
                    ClinicName = d.ClinicName,
                    ClinicAddress = d.ClinicAddress,
                }).ToList();

                return View("~/Views/Admin/Dashboard/Doctors/Index.cshtml", doctorsVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

    }
}
