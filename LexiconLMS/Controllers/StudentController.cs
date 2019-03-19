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

            var course = _context.Courses
                .Include(d => d.Documents)
                .FirstOrDefault(c => c.Users.Contains(user));

            var modules = _context.Modules
                .Include(a => a.Activities)
                .ThenInclude(b => b.Documents)
                .Include(a => a.Documents)
                .Where(a => a.CourseId == course.Id)
                .OrderBy(b => b.StartDate)
                .ThenBy(c => c.EndDate).ToList();

            var teachers = _userManager.GetUsersInRoleAsync("Teacher");
            teachers.Wait();
            var teacher = teachers.Result.Where(a => a.CourseId == course.Id).FirstOrDefault();

            foreach (var m in modules)
            {
                var orderedActivities = m.Activities
                    .OrderBy(a => a.StartDate)
                    .ThenBy(b => b.EndDate)
                    .ToList();

                m.Activities = orderedActivities;
                foreach (var activity in m.Activities)
                {
                    var act = _context.Activities
                        .Include(b => b.ActivityType)
                        .Include(d => d.Documents)
                        .FirstOrDefault(aa => aa.Id == activity.Id);
                    activity.ActivityType = act.ActivityType;
                    //activity.Documents = new List<ActivityDocument>();
                    var documents = _context.ActivityDocument
                        .Where(d => d.ActivityId == act.Id)
                        .Where(d => teachers.Result.Contains(d.User))
                        .ToList();
                    activity.Documents = documents;
                }
            }

            var assignments = _context.ActivityDocument
                .Where(d => teachers.Result.Contains(d.User))
                .Where(d => d.Activityy.Module.Course.Users.Contains(_userManager.GetUserAsync(User).Result))
                .Where(d => d.Activityy.ActivityType.Type == "Exercise")
                .Include(d => d.Activityy)
                .OrderBy(d => d.Activityy.EndDate);

            var myAssignments = _context.ActivityDocument
                .Where(d => d.UserId == _userManager.GetUserId(User));

            var dueAssignments = assignments
                .Where(d => myAssignments.All(e => e.ActivityId != d.ActivityId));

            var model = await SetModelCourseData(course, teacher);
            model = SetModelModulesData(model, modules);
            model = await SetModelStudentsRows(model, user.CourseId);

            model.DueAssignments = _mapper.Map<List<ActivityDocument>, List<AssignmentListViewModel>>(dueAssignments.ToList());
            model.MyAssignments = _mapper.Map<List<ActivityDocument>, List<AssignmentListViewModel>>(myAssignments.ToList());

            return View(model);
        }

        private StudentCourseViewModel SetModelModulesData(StudentCourseViewModel model, List<Module> modules)
        {           
            model.Modules = new List<ModuleDetailsViewModel>();
            modules.ForEach(m => model.Modules.Add(_mapper.Map<ModuleDetailsViewModel>(m)));
            return model;
        }

        private async Task<StudentCourseViewModel> SetModelCourseData(Course course, User teacher)
        {
            var model = new StudentCourseViewModel();
            
            if (!(course is null))
            {
                model.Name = course.Name;
                model.TeacherName = teacher != null ? teacher.FullName : string.Empty;
                model.TeacherEmail = teacher != null ? teacher.Email : string.Empty;

                model.Description = course.Description;

                model.StartDate = course.StartDate;
                model.EndDate = course.EndDate;
                model.Id = course.Id;
                model.Documents = new List<DocumentListViewModel>();
                foreach(var doc in course.Documents)
                {
                    var docName = doc.Name;
                    var maxDocNameLength = 40;
                    if (docName.Length > maxDocNameLength)
                    {
                        docName = docName.Remove(maxDocNameLength) + "...";
                    }
                    model.Documents.Add(new DocumentListViewModel()
                    {
                        Id = doc.Id,
                        Name = docName,
                        Description = doc.Description,
                        UploadTime = doc.UploadTime,
                    });
                }
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

            model.Students = new List<User>();
            foreach (var student in students)
            {
                model.Students.Add(student);
            }

            return model;
        }
    }
}
