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
    internal class TestTypeConfigration : IEntityTypeConfiguration<TestType>
    {

        public void Configure(EntityTypeBuilder<TestType> builder)
        {
            //fluent api for dish 
        }
    }
}
