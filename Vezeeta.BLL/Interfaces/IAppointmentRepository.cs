﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.BLL.DTOs;
using Vezeeta.DAL.Entities;

namespace Vezeeta.BLL.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<AppointmentWithRelatedTablesDto>> GetAppointmentsWithRelatedTablesAsync();
        Task<IEnumerable<AppointmentWithRelatedTablesDto>> GetAppointmentsByDoctorIDWithRelatedTablesAsync(int id);
        Task<IEnumerable<AppointmentWithRelatedTablesDto>> GetAppointmentsByPatientIDWithRelatedTablesAsync(int id);

        Task<AppointmentWithRelatedTablesDto> GetAppointmentWithRelatedTablesAsync(int id);

        Task<IEnumerable<AppointmentStatusCountDto>> GetGetAppointmentsGroupedByStatusAsync();
    }
}
