using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LexiconLMS.Data;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public StudentController(LexiconLMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult>  Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var course = await _context.Courses.FindAsync(user.CourseId);

            var modules = _context.Modules.Include(a=>a.Activities).Where(a => a.CourseId == user.CourseId).OrderBy(b => b.EndDate).ToList();

            var students = await _userManager.GetUsersInRoleAsync("Student");
            var studentsInCourse = students.Where(p => p.CourseId == user.CourseId).ToList();

            var activities = new List<Activityy>();
            //showing Activities

            foreach(var m in modules)
            {
                 activities = _context.Activities.Include(a=>a.Module).Include(a=>a.ActivityType).Where(a => a.ModuleId ==m.Id).ToList();
            }
         

            //
            var model = new StudentCourseViewModel();
            if (!(course is null))
            {
                model.Name = course.Name;
                model.Description = course.Description;

                model.StartDate = course.StartDate;
                model.EndDate = course.EndDate;
                model.Id = course.Id;
            }
            else
            {
                model.Name = "Not in any course!";
            }
            model.Modules = new List<ModuleViewModel>();
            modules.ForEach(m => model.Modules.Add(_mapper.Map<ModuleViewModel>(m)));
            model.Students = StudentsToRows(studentsInCourse);

            //
            model.activities = new List<ActivityViewModel>();
            activities.ForEach(a => model.activities.Add(_mapper.Map<ActivityViewModel>(a)));

            return View(model);
        }

        private static List<List<User>> StudentsToRows(List<User> students)
        {
            var studentsList = new List<List<User>>();

            const int studentsPerRow = 3;
            var row = new List<User>();
            for (var i = 0; i < students.Count; i++)
            {
                var usr = students[i];
                if (i % studentsPerRow == 0)
                {
                    row = new List<User>();
                    studentsList.Add(row);
                }
                row.Add(usr);
            }

            return studentsList;
        }
    }
}
