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
    internal class ClinicConfigration : IEntityTypeConfiguration<Clinic>
    {

        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.HasKey(c => c.ClinicID);

            // Configuring properties
            builder.Property(c => c.Name)
                .IsRequired() // Clinic name is required
                .HasMaxLength(100); // Max length for name

            builder.Property(c => c.Address)
                .IsRequired() // Clinic address is required
                .HasMaxLength(200); // Max length for address


            // one to many (clinic - doctors) --done
            builder.HasMany(c => c.Doctors).
            WithOne(d => d.Clinic)
            .HasForeignKey(d => d.ClinicID);

            // one to many (clinic - appointments) --done
            builder.HasMany(c => c.Appointments)
                .WithOne(a => a.Clinic)
                .HasForeignKey(a => a.ClinicID);
        }
    }
}
