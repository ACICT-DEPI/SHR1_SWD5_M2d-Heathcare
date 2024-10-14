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
            builder.HasOne(c => c.Location)
            .WithOne(l => l.Clinic)
            .HasForeignKey<Clinic>(c => c.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
