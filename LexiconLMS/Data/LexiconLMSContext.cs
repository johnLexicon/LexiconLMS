using LexiconLMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Data
{
    public class LexiconLMSContext : IdentityDbContext<User>
    {
        public LexiconLMSContext(DbContextOptions<LexiconLMSContext> options) : base(options)
        {
        }
    }
}
