using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.BLL.DTOs;
using Vezeeta.BLL.Interfaces;
using Vezeeta.DAL.Context;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Repositories
{
    public class AppointmentRepository : Repository<Appointment> , IAppointmentRepository
    {
        private readonly VezeetaDbContext _dbContext;
        private readonly DbSet<Appointment> _dbSet;
        public AppointmentRepository(VezeetaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Appointment>();
        }
        public async Task<IEnumerable<AppointmentWithRelatedTablesDto>> GetAppointmentsWithRelatedTablesAsync()
        {
            return await _dbSet.Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Clinic).Select(d => new AppointmentWithRelatedTablesDto()
            {
                ID = d.ID,
                Schedule = d.Schedule,
                Note = d.Note,
                Status = d.Status,
                Reason = d.Reason,
                CancellationReason = d.CancellationReason,
                DoctorId = d.DoctorID,
                DoctorName = d.Doctor.FirstName + " " + d.Doctor.LastName,
                PatientId = d.PatientID,
                PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                ClinicId = d.ClinicID,
                ClinicName = d.Clinic.Name,

            }).ToListAsync();
        }

        public async Task<IEnumerable<AppointmentWithRelatedTablesDto>> GetAppointmentsByDoctorIDWithRelatedTablesAsync(int id)
        {
            return await _dbSet.Where(a => a.DoctorID == id).Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Clinic).Select(d => new AppointmentWithRelatedTablesDto()
            {
                ID = d.ID,
                Schedule = d.Schedule,
                Note = d.Note,
                Status = d.Status,
                Reason = d.Reason,
                CancellationReason = d.CancellationReason,
                DoctorId = d.DoctorID,
                DoctorName = d.Doctor.FirstName + " " + d.Doctor.LastName,
                PatientId = d.PatientID,
                PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                ClinicId = d.ClinicID,
                ClinicName = d.Clinic.Name,

            }).ToListAsync();
        }

        public async Task<IEnumerable<AppointmentWithRelatedTablesDto>> GetAppointmentsByPatientIDWithRelatedTablesAsync(int id)
        {
            return await _dbSet.Where(a => a.PatientID == id).Include(a => a.Doctor).Include(a => a.Patient).Include(a => a.Clinic).Select(d => new AppointmentWithRelatedTablesDto()
            {
                ID = d.ID,
                Schedule = d.Schedule,
                Note = d.Note,
                Status = d.Status,
                Reason = d.Reason,
                CancellationReason = d.CancellationReason,
                DoctorId = d.DoctorID,
                DoctorName = d.Doctor.FirstName + " " + d.Doctor.LastName,
                PatientId = d.PatientID,
                PatientName = d.Patient.FirstName + " " + d.Patient.LastName,
                ClinicId = d.ClinicID,
                ClinicName = d.Clinic.Name,

            }).ToListAsync();
        }

        public async Task<AppointmentWithRelatedTablesDto> GetAppointmentWithRelatedTablesAsync(int id)
        {
            var appointment = await _dbSet.Include(a => a.Clinic).Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefaultAsync(a => a.ID == id);

            if(appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            var appointmentDto = new AppointmentWithRelatedTablesDto
            {
                ID = appointment.ID,
                Schedule = appointment.Schedule,
                Status = appointment.Status,
                Note = appointment.Note,
                Reason = appointment.Reason,
                CancellationReason = appointment.CancellationReason,
                DoctorName = appointment.Doctor != null
                ? $"{appointment.Doctor.FirstName} {appointment.Doctor.LastName}"
                : "Doctor information not available",
                PatientName = appointment.Patient != null
                ? $"{appointment.Patient.FirstName} {appointment.Patient.LastName}"
                : "Patient information not available",
                ClinicName = appointment.Clinic?.Name ?? "Clinic information not available"
            };
            return appointmentDto;

        }
       
        public async Task<IEnumerable<AppointmentStatusCountDto>> GetGetAppointmentsGroupedByStatusAsync()
        {
            return await _dbSet.GroupBy(a => a.Status).Select(g => new AppointmentStatusCountDto
            {
                Status = g.Key,
                Count = g.Count()

            }).ToListAsync();
        }
    }
}
