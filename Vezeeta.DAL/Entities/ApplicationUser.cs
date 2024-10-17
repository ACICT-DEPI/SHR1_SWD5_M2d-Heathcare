﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {


        //public string FullName => $"{FirstName} {LastName}";
        public string FullName { get; set; }
    }
}
