﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class Example
    {
        [Key]
        public int Id { get; set; }
        public  string Name { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; } = null;

    }
}
