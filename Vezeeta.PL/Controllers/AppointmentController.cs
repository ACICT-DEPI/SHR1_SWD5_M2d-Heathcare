using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Numerics;
using System.Security.Claims;
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
        private async Task<List<SelectListItem>> GetClinicsWithDoctorsAsync()
        {
            var clinics = await _unitOfWork.ClinicRepository.GetAllWithDoctorsAsync();
            var selectItems = new List<SelectListItem>();
            foreach (var clinic in clinics)
            {

                var clinicGroup = new SelectListGroup { Name = clinic.Name };
                foreach (var doctor in clinic.Doctors)
                {
                    var valueWithClinic = $"{clinic.ClinicID},{doctor.DoctorID}";
                    selectItems.Add(new SelectListItem { Text = doctor.FirstName + " " + doctor.LastName, Value = valueWithClinic, Group = clinicGroup });
                }
            }
            return selectItems;
        }

        private async Task<AppointmentVM> GetAppointmentWithRelatedData(int id)
        {
            var appointmentDTO = await _unitOfWork.AppointmentRepository.GetAppointmentWithRelatedTablesAsync(id);
            var appointmentVM = new AppointmentVM
            {
                ID = appointmentDTO.ID,
                CancellationReason = appointmentDTO.CancellationReason,
                Note = appointmentDTO.Note,
                Reason = appointmentDTO.Reason,
                Schedule = appointmentDTO.Schedule,
                Status = appointmentDTO.Status,
                ClinicName = appointmentDTO.ClinicName,
                DoctorName = appointmentDTO.DoctorName,
                PatientName = appointmentDTO.PatientName,
                
            };
            return appointmentVM;
        }

        private async Task<Patient> GetCurrentPatient()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var patient = await _unitOfWork.PatientRepository.FindByUserIdAsync(userId);

            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient not found.");
            }
            return patient;

        }

        private async Task<Doctor> GetCurrentDoctor()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var doctor = await _unitOfWork.DoctorRepository.FindByUserIdAsync(userId);

            if (doctor == null)
            {
                throw new KeyNotFoundException($"Patient not found.");
            }
            return doctor;

        }

        // GET: Appointments for admin
        [Authorize(Roles ="Admin")]
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

                return View(appointmnetsVM);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // Get Appointments for doctor
        public async Task<IActionResult> Doctor()
        {
            try
            {
                var doctor = await GetCurrentDoctor();
                var appointmnets = await _unitOfWork.AppointmentRepository.GetAppointmentsByDoctorIDWithRelatedTablesAsync(doctor.DoctorID);
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
                return View(appointmnetsVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // Get Appointments for patinet
        public async Task<IActionResult> Patient()
        {
            try
            {
                var patient = await GetCurrentPatient();
                var appointmnets = await _unitOfWork.AppointmentRepository.GetAppointmentsByPatientIDWithRelatedTablesAsync(patient.PatientID);
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
                return View(appointmnetsVM);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: Appointment / 1

        [HttpGet]
        [Authorize(Roles = "Patient, Doctor, Admin")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var appointmentVM = await GetAppointmentWithRelatedData(id);
                return View(appointmentVM);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: Appointment/Create
        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Create()
        {
            var bookAppointmentVM = new BookAppointmentVM
            {
                DoctorsSelectList = await GetClinicsWithDoctorsAsync(),
                Schedule = DateTime.Now,
            };
            return View( bookAppointmentVM);
        }

        // post: Appointment/Create
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Create(BookAppointmentVM appointmentVM)
        {
            if (appointmentVM == null)
            {
                return BadRequest();
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    appointmentVM.DoctorsSelectList = await GetClinicsWithDoctorsAsync();
                    return View(appointmentVM);
                }

                var patient = await GetCurrentPatient();

                var doctorAndClinic = appointmentVM.SelectedDoctorId.Split(',');

                var clinicID = int.Parse(doctorAndClinic[0]);
                var doctorID = int.Parse(doctorAndClinic[1]);

                var appointment = new Appointment
                {
                    Schedule = appointmentVM.Schedule,
                    Note = appointmentVM.Note,
                    Reason = appointmentVM.Reason,
                    CancellationReason = "",
                    Status = "pending",
                    DoctorID = doctorID,
                    PatientID = patient.PatientID,
                    ClinicID = clinicID,

                };
                await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Patient));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        
        
        // GET: Appointment/Edit/5
        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);

                if (appointment == null) 
                { 
                    return NotFound();
                }
                var patient = await GetCurrentPatient();

                var bookAppointmentVM = new BookAppointmentVM
                {
                    ID = appointment.ID,
                    Schedule = appointment.Schedule,
                    Reason = appointment.Reason,
                    Note = appointment.Note,
                    SelectedDoctorId = $"{appointment.ClinicID},{appointment.DoctorID}",
                    DoctorsSelectList = await GetClinicsWithDoctorsAsync(),
                };

                return View(bookAppointmentVM);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [Authorize(Roles = "Patient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookAppointmentVM bookAppointmentVM)
        {
            if (id != bookAppointmentVM.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAppointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                    if (existingAppointment == null)
                    {
                        return NotFound(); 
                    }

                    var doctorAndClinic = bookAppointmentVM.SelectedDoctorId.Split(',');
                    var clinicID = int.Parse(doctorAndClinic[0]);
                    var doctorID = int.Parse(doctorAndClinic[1]);
                    existingAppointment.Schedule = bookAppointmentVM.Schedule;
                    existingAppointment.Reason = bookAppointmentVM.Reason;
                    existingAppointment.Note = bookAppointmentVM.Note;
                    existingAppointment.ClinicID = clinicID;
                    existingAppointment.DoctorID = doctorID;

                     _unitOfWork.Repository<Appointment>().UpdateAsync(existingAppointment);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Patient));

                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
            return View(bookAppointmentVM);
        }


        // Get : Cancel / 5
        [HttpGet]
        [Authorize(Roles = "Patient, Doctor")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                var appointmentMV = new CancelAppointmentVM
                { 
                    ID = appointment.ID,
                    CancellationReason = appointment.CancellationReason,
                };
                return View(appointmentMV);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // Post : Cancel / 5

        [HttpPost]
        [Authorize(Roles = "Patient, Doctor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, CancelAppointmentVM appointmentMV)
        {
            if (id != appointmentMV.ID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(appointmentMV);
            }
                try
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                appointment.Status = "cancelled";
                appointment.CancellationReason = appointmentMV.CancellationReason;
                _unitOfWork.Repository<Appointment>().UpdateAsync(appointment);
                await _unitOfWork.SaveAsync();
                if (User.IsInRole("Patient"))
                {
                    return RedirectToAction(nameof(Patient));
                }else if (User.IsInRole("Doctor"))
                {
                    return RedirectToAction(nameof(Doctor));
                }else
                    return Unauthorized();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        

        // Get : Schedule / 5
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Schedule(int id)
        {
            try
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                var appointmentMV = new ScheduleAppointmentVM
                {
                    ID = appointment.ID,
                    Schedule = appointment.Schedule,
                };
                return View(appointmentMV);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule(int id, ScheduleAppointmentVM appointmentMV)
        {
            if (id != appointmentMV.ID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(appointmentMV);
            }
            try
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                appointment.Status = "scheduled";
                appointment.Schedule = appointmentMV.Schedule;
                appointment.Note = appointmentMV.Note;
                _unitOfWork.Repository<Appointment>().UpdateAsync(appointment);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Doctor));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
       
        
        // Get : Delete / 5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                var appointmentMV = new AppointmentVM
                {
                    ID = appointment.ID,
                };

                return View(appointmentMV);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, AppointmentVM appointmentMV)
        {
            if (id != appointmentMV.ID)
            {
                return BadRequest();
            }
            try
            {
                var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Repository<Appointment>().DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


    }
}
