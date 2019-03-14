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

            var course = await _context.Courses
                .FindAsync(user.CourseId);

            var modules = _context.Modules
                .Include(a => a.Activities)
                .Include(a => a.Documents)
                .Where(a => a.CourseId == course.Id)
                .OrderBy(b => b.StartDate)
                .ThenBy(c => c.EndDate).ToList();


            foreach (var m in modules)
            {
                var orderedActivities = m.Activities.OrderBy(a => a.StartDate).ThenBy(b => b.EndDate).ToList();
                m.Activities = orderedActivities;
                foreach (var activity in m.Activities)
                {
                    var act = _context.Activities.Include(b => b.ActivityType).FirstOrDefault(aa => aa.Id == activity.Id);
                    activity.ActivityType = act.ActivityType;
                }
            }

            var model = await SetModelCourseData(course);
            model = SetModelModulesData(model, modules);
            model = await SetModelStudentsRows(model, user.CourseId);
            return View(model);
        }

        private StudentCourseViewModel SetModelModulesData(StudentCourseViewModel model, List<Module> modules)
        {           
            model.Modules = new List<ModuleAddViewModel>();
            modules.ForEach(m => model.Modules.Add(_mapper.Map<ModuleAddViewModel>(m)));
            return model;
        }

        private async Task<StudentCourseViewModel> SetModelCourseData(Course course)
        {
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
            return model;
        }

        private async Task<StudentCourseViewModel> SetModelStudentsRows(StudentCourseViewModel model, int? courseId)
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");
            var studentsInCourse = students.Where(p => p.CourseId == courseId).ToList();

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
            model.Students = studentsList;

            return model;
        }
    }
}
