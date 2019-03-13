using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Humanizer;
using Humanizer.Bytes;
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
                if (!(teacher is null))
                {
                    participants.Add(teacher);
                }

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

            viewModel.Documents = new List<DocumentListViewModel>();
            var documents = _context.CourseDocument.Where(d => d.CourseId == id).ToList();
            foreach(var doc in documents)
            {
                var newDoc = _mapper.Map<DocumentListViewModel>(doc);
                newDoc.Filezise = (doc.DocumentData.Length).Bytes().Humanize("#.#");
                viewModel.Documents.Add(newDoc);
            }

            viewModel.Students = course.Users.Except(theTeacher);
            

            viewModel.Modules = new List<ModuleViewModel>();
            var modules = _context.Modules.Where(a => a.CourseId == id).ToList();
            modules.ForEach(a => viewModel.Modules.Add(_mapper.Map<ModuleViewModel>(a)));

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);
            var teachers = _userManager.GetUsersInRoleAsync("Teacher");
            teachers.Wait();

            if (course is null)
            {
                return NotFound();
            }

            var viewModel = new AddCourseViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
            };

            var teacher = _userManager.GetUsersInRoleAsync("Teacher");
            teacher.Wait();

            var theTeacher = course.Users.Intersect(teacher.Result);

            if (theTeacher.Count() < 1)
            {
                theTeacher = new List<User>() { new User() { Email = "not assigned" } };
            }

            viewModel.Teachers = teachers.Result.Select(t => new Tuple<string, string>(t.Id, t.UserName)).ToList();
            viewModel.TeacherId = theTeacher.FirstOrDefault().Id;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddCourseViewModel viewModel)
        {

            if (ModelState.IsValid)
            {

                var course = _context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == viewModel.Id);
                var teacher = _userManager.Users.FirstOrDefault(u => u.Id == viewModel.TeacherId);

                course.EndDate = viewModel.EndDate;
                course.StartDate = viewModel.StartDate;
                course.Name = viewModel.Name;
                course.Description = viewModel.Description;

                List<User> participants = new List<User>();
                if (!(teacher is null))
                {
                    participants.Add(teacher);
                }
                course.Users = participants;

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

        public async Task<IActionResult> Delete(int id)
        {
            Course courseToDelete = await _context.Courses.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == id);
            
            if (courseToDelete is null)
            {
                return NotFound();
            }

            var students = await _userManager.GetUsersInRoleAsync("Student");
            var studentsInCourse = students.Where(s => s.CourseId == courseToDelete.Id);

            _context.Remove(courseToDelete);
            _context.RemoveRange(studentsInCourse);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}