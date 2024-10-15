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
    internal class DoctorSpecializationConfigration : IEntityTypeConfiguration<DoctorSpecialization>
    {

        public void Configure(EntityTypeBuilder<DoctorSpecialization> builder)
        {
            //composite key
            builder.HasKey(ds => new { ds.DoctorID, ds.SpecializationID });


            builder
            .HasOne(ds => ds.Doctor)
            .WithMany(d => d.DoctorSpecializations)
            .HasForeignKey(ds => ds.DoctorID);

            builder
            .HasOne(ds => ds.Specialization)
            .WithMany(s => s.DoctorSpecializations)
            .HasForeignKey(ds => ds.SpecializationID);
        }
    }
}
