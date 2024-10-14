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
            //fluent api for dish 
        }
    }
}
