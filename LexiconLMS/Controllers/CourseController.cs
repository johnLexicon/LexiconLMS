using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LexiconLMS.Data;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexiconLMS.Controllers
{

    [Authorize(Roles = "Teacher")]
    public class CourseController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;

        public CourseController(LexiconLMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseViewModel viewModel)
        {


            if (ModelState.IsValid)
            {
                Course course = _mapper.Map<Course>(viewModel);
                await _context.Courses.AddAsync(course);
                _context.SaveChanges();

                return RedirectToAction(nameof(Details), new { course.Id });
            }

            return View();
        }

        public IActionResult Details(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if(course is null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CourseDetailsViewModel>(course);

            return View(viewModel);
        }
    }
}