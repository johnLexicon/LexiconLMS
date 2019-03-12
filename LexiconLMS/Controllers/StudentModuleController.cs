using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LexiconLMS.Data;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentModuleController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;

        public StudentModuleController(LexiconLMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules.FirstOrDefaultAsync(a => a.Id == id);

            if (module is null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(a => a.Id == module.CourseId);

            if (course is null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ModuleViewModel>(@module);
            model.CourseId = course.Id;
            model.CourseName = course.Name;



            model.Activities = new List<ActivityViewModel>();
            var activities = _context.Activities.Include(a => a.Module).Include(a => a.ActivityType).Where(a => a.ModuleId == id).ToList();
            activities.ForEach(a => model.Activities.Add(_mapper.Map<ActivityViewModel>(a)));

            return View(model);
        }

    }
}
