﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class User : IdentityUser
    {
        public Course Course { get; set; }
        [Display(Name="Name")]
        public string FullName { get; set; }
    }
}
