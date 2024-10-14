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
    internal class AllergyConfigration : IEntityTypeConfiguration<Allergy>
    {

        public void Configure(EntityTypeBuilder<Allergy> builder)
        {
            //fluent api for dish 
        }
    }
}
