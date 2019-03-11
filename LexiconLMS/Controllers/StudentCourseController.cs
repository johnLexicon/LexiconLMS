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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentCourseController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public StudentCourseController(LexiconLMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;

        }


        public IActionResult Details(int id)
        {
            var course = _context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);

            if (course is null)
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

            viewModel.Documents = _context.CourseDocument.Where(d => d.CourseId == id).ToList();

            viewModel.Students = course.Users.Except(theTeacher);


            viewModel.Modules = new List<ModuleViewModel>();
            var modules = _context.Modules.Where(a => a.CourseId == id).ToList();
            modules.ForEach(a => viewModel.Modules.Add(_mapper.Map<ModuleViewModel>(a)));

            return View(viewModel);
        }


    }
}
