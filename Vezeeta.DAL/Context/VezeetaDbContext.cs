using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Vezeeta.DAL.Entities;
using Project.Entites;

namespace Vezeeta.DAL.Context
{
    public class VezeetaDbContext : DbContext
    {
        public VezeetaDbContext(DbContextOptions<VezeetaDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());// get all classes that implement IEntityTypeConfigurations
        }


        //add dbset 
        public DbSet<Example> Examples { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TestType> TestTypes { get; set; }



    }
}
