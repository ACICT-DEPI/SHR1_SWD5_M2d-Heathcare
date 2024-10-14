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




        }
    }
}
