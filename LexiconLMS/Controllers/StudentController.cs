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

            var modules = _context.Modules.Where(a => a.CourseId == user.CourseId).OrderBy(b=>b.EndDate).ToList();

            var model = new StudenCourseViewModel();
            if (!(course is null))
            {
                model.Name = course.Name;
                model.Description = course.Description;

                model.StartDate = course.StartDate;
                model.EndDate = course.EndDate;
                model.Id = course.Id;
            } else
            {
                model.Name = "Not in any course!";
            }
            model.Modules = new List<ModuleViewModel>();
            modules.ForEach(m => model.Modules.Add(_mapper.Map<ModuleViewModel>(m)));

            return View(model);
        }
    }
}
