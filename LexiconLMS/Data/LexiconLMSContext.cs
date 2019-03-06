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

        /*** Properties of DbSets for entities that you want to interact with directly ***/

        public DbSet<Course> Courses { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Activityy> Activities { get; set; }

        public DbSet<ActivityType> ActivityType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Course>()
            //    .HasOne<User>(c => c.Teacher) //Needed for creating a foreign key to the Teacher.
            //    .WithOne(d => d.Course)
            //    .HasForeignKey<User>(e => e.CourseId);

            builder.Entity<Module>();

            builder.Entity<Course>()
                .HasMany<User>(u => u.Users);
                
            

            builder.Entity<User>()
                .HasOne<Course>(c => c.Course);

            builder.Entity<Module>();

            builder.Entity<Activityy>();
            builder.Entity<ActivityType>();



        }     
    }
}
