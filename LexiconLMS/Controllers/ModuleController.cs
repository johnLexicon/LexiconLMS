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

            var model = _mapper.Map<ModuleViewModel>(@module);
            model.CourseId = course.Id;
            model.CourseName = course.Name;

            

            model.Activities = new List<ActivityViewModel>();
            var activities = _context.Activities.Include(a=>a.Module).Include(a=>a.ActivityType).Where(a => a.ModuleId == id).ToList();
            activities.ForEach(a => model.Activities.Add(_mapper.Map<ActivityViewModel>(a)));

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

            var model = _mapper.Map<ModuleViewModel>(new Module());
            model.CourseId = course.Id;
            model.CourseName = course.Name;
            var dateTimeNow = DateTime.Now;
            model.StartDate = dateTimeNow;
            model.EndDate = dateTimeNow.AddDays(7);
            return View(model);
        }

        // POST: Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,StartDate,EndDate,DocId,CourseId")] ModuleViewModel @module)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Module>(@module);
                _context.Add(model);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            return View(@module);
        }


        // GET: Module/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var module = await _context.Modules.FirstOrDefaultAsync(a => a.Id == id);
            if (!(module is null))
            {
                _context.Remove(module);
                _context.SaveChanges();

                var course = await _context.Courses.FirstOrDefaultAsync(a => a.Id == module.CourseId);
                if (!(course is null))
                {
                    return RedirectToAction("Details", "Course", new { id = course.Id });
                }
            }

            return NotFound();
        }


        // GET: Module/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var module = await _context.Modules.FirstOrDefaultAsync(a => a.Id == id);
            if (!(module is null))
            {
                var course = await _context.Courses.FirstOrDefaultAsync(a => a.Id == module.CourseId);
                if (!(course is null))
                {
                    var model = _mapper.Map<Module, ModuleViewModel>(module);
                    model.CourseId = course.Id;
                    model.CourseName = course.Name;
                    return View(model);
                }
            }

            return NotFound();
        }


        // POST: Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, Name, Description, StartDate, EndDate, CourseId")] ModuleViewModel @module)
        {
            if (ModelState.IsValid)
            {
                var moduleEntity = await _context.Modules.FirstOrDefaultAsync(a => a.Id == @module.Id);

                moduleEntity.Name = @module.Name;
                moduleEntity.StartDate = @module.StartDate;
                moduleEntity.EndDate = @module.EndDate;
                moduleEntity.Description = @module.Description;

                _context.Update(moduleEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = moduleEntity.Id });
            }

            return NotFound();
        }
    }
}
