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
    internal class PhoneConfigration : IEntityTypeConfiguration<Phone>
    {

        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            //fluent api for dish 
        }
    }
}
