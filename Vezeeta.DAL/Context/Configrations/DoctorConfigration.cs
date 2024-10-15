using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.DAL.Entities;

namespace Vezeeta.DAL.Context.Configrations
{
    internal class DoctorConfigration : IEntityTypeConfiguration<Doctor>
    {

        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.DoctorID);

            builder.Property(d => d.FirstName)
                .IsRequired() 
                .HasMaxLength(50); 

            builder.Property(d => d.LastName)
                .IsRequired() 
                .HasMaxLength(50);

            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(100);
               

            builder.Property(d => d.Phone)
                .IsRequired() 
                .HasMaxLength(15); 


            // one to many (doctor - appointments)  --done
            builder.HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorID).
                OnDelete(DeleteBehavior.NoAction);


        }
    }
}
