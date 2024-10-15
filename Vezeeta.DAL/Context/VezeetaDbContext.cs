using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Vezeeta.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using Vezeeta.DAL.Context.Configrations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vezeeta.DAL.Context
{
    public class VezeetaDbContext : DbContext
    {
        public VezeetaDbContext(DbContextOptions<VezeetaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Admin>(new AdminConfigration());
            modelBuilder.ApplyConfiguration<Doctor>(new DoctorConfigration());
            modelBuilder.ApplyConfiguration<Patient>(new PatientConfigration());
            modelBuilder.ApplyConfiguration<Appointment>(new AppointmentConfigration());
            modelBuilder.ApplyConfiguration<Clinic>(new ClinicConfigration());
            modelBuilder.ApplyConfiguration<Specialization>(new SpecializationConfigration());
            modelBuilder.ApplyConfiguration<DoctorSpecialization>(new DoctorSpecializationConfigration());


            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());// get all classes that implement IEntityTypeConfigurations
        }


        //add dbset 

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        public DbSet<Patient> Patients { get; set; }


    }


}
