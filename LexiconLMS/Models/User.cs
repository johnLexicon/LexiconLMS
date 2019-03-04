using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class User : IdentityUser
    {
        public Course Course { get; set; }
        public string FullName { get; set; }
    }
}
