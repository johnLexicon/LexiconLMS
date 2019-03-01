using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LexiconLMS.Data;
using LexiconLMS.Models;
using Microsoft.AspNetCore.Authorization;
using LexiconLMS.ViewModels;
using AutoMapper;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ModuleController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;

        public ModuleController(LexiconLMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       

        // GET: Module/Details/5
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

            //var model = _mapper.Map<ModuleViewModel>(@module);
            var model = new ModuleViewModel(@module);
            model.CourseId = course.Id;
            model.Name = course.Name;
            return View(model);
        }

        // GET: Module/Create
        public async Task<IActionResult> Create(int id)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(a => a.Id == id);

            if (course is null)
            {
                return NotFound();
            }

            //var model = _mapper.Map<ModuleViewModel>(new Module());
            var model = new ModuleViewModel(new Module());
            model.CourseId = course.Id;
            model.CourseName = course.Name;
            return View(model);
        }

        // POST: Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,StartDate,EndDate,DocId,CourseId")] Module @module)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { module.Id });
            }
            return View(@module);
        }
    }
}
