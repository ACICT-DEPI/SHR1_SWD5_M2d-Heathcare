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
    internal class AppointmentConfigration : IEntityTypeConfiguration<Appointment>
    {

        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.ID);

            builder.Property(a => a.Schedule)
                .IsRequired();

            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.ToTable(t => t.HasCheckConstraint("CHK_Status", "[Status] IN ('scheduled', 'pending', 'cancelled')"));

            builder.Property(a => a.Note)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.Reason)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.CancellationReason)
                .IsRequired(false);
        }
    }
}
