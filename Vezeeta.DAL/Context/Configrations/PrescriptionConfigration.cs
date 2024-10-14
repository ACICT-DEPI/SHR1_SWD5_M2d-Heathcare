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
    internal class PrescriptionConfigration : IEntityTypeConfiguration<Prescription>
    {

        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            //fluent api for dish 
        }
    }
}
