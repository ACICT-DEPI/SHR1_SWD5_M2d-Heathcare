using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.DAL.Entities;

namespace Vezeeta.DAL.Context.Configrations
{
    internal class PatientConfigration : IEntityTypeConfiguration<Patient>
    {

        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder.HasKey(p => p.PatientID);

            builder.Property(p => p.FirstName)
                .IsRequired() 
                .HasMaxLength(50); 

            builder.Property(p => p.LastName)
                .IsRequired() 
                .HasMaxLength(50); 

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100); 
     

            builder.Property(p => p.Phone)
                .IsRequired() 
                .HasMaxLength(15);

            builder.Property(p => p.DateOfBirth)
                .IsRequired();


            // one to many (patient - appointments) --done
            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientID);
        }
    }
}
