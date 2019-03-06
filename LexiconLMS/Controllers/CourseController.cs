using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LexiconLMS.Data;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexiconLMS.Controllers
{

    [Authorize(Roles = "Teacher")]
    public class CourseController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CourseController(LexiconLMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var courses = _context.Courses.Include(u => u.Users);
            //var viewModels = await courses.ProjectTo<CourseListViewModel>(_mapper.ConfigurationProvider).ToListAsync();

            var Teachers = _userManager.GetUsersInRoleAsync("Teacher");
            Teachers.Wait();

            List<CourseListViewModel> viewModels = new List<CourseListViewModel>();
            foreach (var course in courses)
            {

                if(course.Users is null)
                {
                    course.Users = new List<User>();
                }
                var theTeacher = course.Users.Intersect(Teachers.Result);

                if (theTeacher.Count() < 1)
                {
                    theTeacher = new List<User>() { new User() { FullName = "not assigned" } };
                }


                var vm = new CourseListViewModel()
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    TeacherName = theTeacher.First().FullName,
                    NumberOfStudents = course.Users.Except(theTeacher).Count(),
                };
                viewModels.Add(vm);
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            var students = await _userManager.GetUsersInRoleAsync("Student");

            var startDate = DateTime.Now;
            AddCourseViewModel viewModel = new AddCourseViewModel
            {
                Teachers = teachers.Select(t => new Tuple<string, string>(t.Id, t.UserName)).ToList(),
                StartDate = startDate,
                Students = students.Where(u => u.CourseId is null).Select(t => new Tuple<string, string>(t.Id, t.UserName)).ToList(),
                EndDate = startDate.AddMonths(1)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                var teacher = _userManager.Users.FirstOrDefault(u => u.Id == viewModel.TeacherId);
                Course course = _mapper.Map<Course>(viewModel);

                List<User> participants = new List<User>();

                participants.Add(teacher);
               
                var students = _userManager.Users.Where(u => viewModel.StudentIds.Contains(u.Id));

                participants.AddRange(students);
                course.Users = participants;

                await _context.Courses.AddAsync(course);
                _context.SaveChanges();

                return RedirectToAction(nameof(Details), new { course.Id });
            }
            else
            {
                var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
                viewModel.Teachers = teachers.Select(t => new Tuple<string, string>(t.Id, t.UserName)).ToList();
            }

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var course = _context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);

            if(course is null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CourseDetailsViewModel>(course);

            var teacher = _userManager.GetUsersInRoleAsync("Teacher");
            teacher.Wait();

            var theTeacher = course.Users.Intersect(teacher.Result);

            if (theTeacher.Count() < 1)
            {
                theTeacher = new List<User>() { new User() { Email = "not assigned" } };
            }

            viewModel.TeacherEmail = theTeacher.FirstOrDefault().Email;
            viewModel.Students = course.Users.Except(theTeacher);
            if(viewModel.Students.Count() < 1)
            {
                viewModel.Students = new List<User>() { new User() { Email = "none" } };
            }

            viewModel.Modules = new List<ModuleViewModel>();
            var modules = _context.Modules.Where(a => a.CourseId == id).ToList();
            modules.ForEach(a => viewModel.Modules.Add(_mapper.Map<ModuleViewModel>(a)));

            return View(viewModel);
        }
    }
}