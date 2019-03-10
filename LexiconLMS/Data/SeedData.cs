using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LexiconLMS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LexiconLMS.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<LexiconLMSContext>>();
            using (var context = new LexiconLMSContext(options))
            {
                try
                {
                    if (context.ActivityType.Any())
                    {
                        return;
                    }

                    // Let's seed!
                    var ActivityTypes = new List<ActivityType>()
                    { new ActivityType() { Type = "E-Learning"},
                    new ActivityType() { Type = "Lectures"},
                    new ActivityType() { Type = "Exercise" },
                    };
                    //people.Add(person);

                    context.ActivityType.AddRange(ActivityTypes);
                    context.SaveChanges();
                }
                catch
                {
                    //Table ActivityType should exist, but if it doesn't, don't crash the application completely.
                }
            }
        }

        public static void SeedCourseData(ModelBuilder builder)
        {
            builder.Entity<Course>()
                .HasData(
                    new Course
                    {
                        Id = -1,
                        Name = "Programmerare och systemutvecklare Inriktning Microsoft .NET",
                        StartDate = new DateTime(2018, 11, 26),
                        EndDate = new DateTime(2019, 03, 22),
                        Description = "Utbildningen mot programmerare och systemutvecklare syftar till att skapa förutsättningar att ut-veckla kunskaper och färdigheter i programmering och att utveckla IT-system, applikationer eller delar av system. Utbildningen syftar till att inom valt språk täcka systemutveckling, frontend, backend, fullstack samt mobil applikationsutveckling."
                    }
                );

            builder.Entity<Module>()
                .HasData(
                    new Module { Id = -1, Name = "Programmering", Description = "Lorem ipsum dolor sit amet", StartDate = new DateTime(2018, 11, 26), EndDate = new DateTime(2018, 12, 07), CourseId = -1 },
                    new Module { Id = -2, Name = "Avancerad Programmering", Description = "Cras ut euismod enim", StartDate = new DateTime(2018, 12, 10), EndDate = new DateTime(2019, 01, 02), CourseId = -1 },
                    new Module { Id = -3, Name = "Databas", Description = "Ut a lobortis eros, at blandit metu", StartDate = new DateTime(2018, 01, 31), EndDate = new DateTime(2019, 02, 08), CourseId = -1 },
                    new Module { Id = -4, Name = "FrontEnd", Description = "Vestibulum pharetra ultrices pulvinar", StartDate = new DateTime(2019, 01, 03), EndDate = new DateTime(2019, 01, 11), CourseId = -1 },
                    new Module { Id = -5, Name = "BackEnd", Description = "Fusce semper, tortor ac condimentum", StartDate = new DateTime(2019, 02, 22), EndDate = new DateTime(2019, 03, 01), CourseId = -1 },
                    new Module { Id = -6, Name = "Applikationsutveckling", Description = "Vestibulum sit amet magna turpis", StartDate = new DateTime(2019, 03, 02), EndDate = new DateTime(2019, 03, 15), CourseId = -1 },
                    new Module { Id = -7, Name = "Testning av mjukvara", Description = "Nunc libero quam, varius id mattis ut", StartDate = new DateTime(2019, 03, 16), EndDate = new DateTime(2019, 03, 22), CourseId = -1 }
                );

            builder.Entity<Activityy>()
                .HasData(
                    new Activityy { Id = -1, ActivityTypeId = 1, StartDate = new DateTime(2018, 11, 26, 8, 0, 0), EndDate = new DateTime(2018, 11, 26, 12, 15, 0), ModuleId = -1, Description = "Mauris venenatis" },
                    new Activityy { Id = -2, ActivityTypeId = 2, StartDate = new DateTime(2018, 11, 26, 13, 15, 0), EndDate = new DateTime(2018, 11, 26, 17, 0, 0), ModuleId = -1, Description = "Nunc tempus finibus mollis" },
                    new Activityy { Id = -3, ActivityTypeId = 3, StartDate = new DateTime(2018, 11, 27, 8, 0, 0), EndDate = new DateTime(2018, 11, 27, 17, 0, 0), ModuleId = -1, Description = "Mauris venenatis" },
                    new Activityy { Id = -4, ActivityTypeId = 1, StartDate = new DateTime(2018, 11, 28, 8, 0, 0), EndDate = new DateTime(2018, 11, 28, 17, 0, 0), ModuleId = -1, Description = "Nunc tempus finibus mollis" },
                    new Activityy { Id = -5, ActivityTypeId = 2, StartDate = new DateTime(2018, 11, 29, 8, 0, 0), EndDate = new DateTime(2018, 11, 29, 17, 0, 0), ModuleId = -1, Description = "Mauris venenatis" },
                    new Activityy { Id = -6, ActivityTypeId = 2, StartDate = new DateTime(2018, 11, 30, 8, 0, 0), EndDate = new DateTime(2018, 11, 30, 17, 0, 0), ModuleId = -1, Description = "Nunc tempus finibus mollis" },
                    new Activityy { Id = -7, ActivityTypeId = 1, StartDate = new DateTime(2018, 12, 01, 8, 0, 0), EndDate = new DateTime(2018, 12, 26, 12, 15, 0), ModuleId = -1, Description = "Mauris venenatis" },
                    new Activityy { Id = -8, ActivityTypeId = 2, StartDate = new DateTime(2018, 12, 01, 13, 15, 0), EndDate = new DateTime(2018, 12, 01, 17, 0, 0), ModuleId = -1, Description = "Nunc tempus finibus mollis" },
                    new Activityy { Id = -9, ActivityTypeId = 3, StartDate = new DateTime(2018, 12, 02, 8, 0, 0), EndDate = new DateTime(2018, 12, 02, 17, 0, 0), ModuleId = -1, Description = "Mauris venenatis" },
                    new Activityy { Id = -10, ActivityTypeId = 1, StartDate = new DateTime(2018, 12, 03, 8, 0, 0), EndDate = new DateTime(2018, 12, 03, 15, 30, 0), ModuleId = -1, Description = "Nunc tempus finibus mollis" },
                    new Activityy { Id = -11, ActivityTypeId = 2, StartDate = new DateTime(2018, 12, 04, 8, 0, 0), EndDate = new DateTime(2018, 12, 04, 17, 15, 0), ModuleId = -1, Description = "Mauris venenatis" },
                    new Activityy { Id = -12, ActivityTypeId = 3, StartDate = new DateTime(2018, 12, 05, 8, 0, 0), EndDate = new DateTime(2018, 12, 05, 16, 0, 0), ModuleId = -1, Description = "Nunc tempus finibus mollis" }
                );
        }

        public static void SeedCourseParticipants(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<LexiconLMSContext>>();

            using (var context = new LexiconLMSContext(options))
            {
                Course course = context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == -1);

                if(course is null)
                {
                    return;
                }

                if (course.Users != null && course.Users.Any())
                {
                    return;
                }

                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var mapper = serviceProvider.GetRequiredService<IMapper>();
                var config = serviceProvider.GetRequiredService<IConfiguration>();

                var passwordForParticipants = config["LexiconLMS:SeededCourse:ParticipantPW"];
                var localeCode = config["LexiconLMS:SeededCourse:LocaleCode"];
                int.TryParse(config["LexiconLMS:SeededCourse:StudentsCount"], out int studentsCount);

                //Create students
                for (var i = 0; i < studentsCount; i++)
                {
                    var bogusPerson = new Bogus.Person(localeCode);
                    User student = mapper.Map<User>(bogusPerson);
                    student.CourseId = course.Id;
                    CreateUser(userManager, student, "Student", passwordForParticipants);
                }

                //CreateUser teacher
                User teacher = mapper.Map<User>(new Bogus.Person(localeCode));
                teacher.CourseId = course.Id;
                CreateUser(userManager, teacher, "Teacher", passwordForParticipants);

            }
        }

        private static void CreateUser(UserManager<User> userManager, User user, string roleName, string password = "secret123")
        {

            Task<IdentityResult> createUserTask = userManager.CreateAsync(user, password);
            createUserTask.Wait();

            //If the admin user was succesfully created it adds the Administrator role to the user.
            if (createUserTask.Result.Succeeded)
            {
                Task<IdentityResult> addToRoleResult = userManager.AddToRoleAsync(user, roleName);
                addToRoleResult.Wait();
            }
        }
    }
}
