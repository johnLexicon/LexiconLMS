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
using Humanizer;

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

            //var model = _mapper.Map<ModuleDetailsViewModel>(@module);

            var model = new ModuleDetailsViewModel()
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Id = module.Id,
                Name = module.Name,
                Description = module.Description,
                StartDate = module.StartDate,
                EndDate = module.EndDate,
                ParentStartDate = course.StartDate,
                ParentEndDate = course.EndDate
            };
            model.CourseId = course.Id;
            model.CourseName = course.Name;



            model.Activities = new List<ActivityDetailsViewModel>();
            var activities = _context.Activities
                .Include(a=>a.Module)
                .Include(a=>a.ActivityType)
                .Where(a => a.ModuleId == id)
                .OrderBy(d => d.StartDate)
                .ThenBy(e => e.EndDate).ToList();
            activities.ForEach(a => model.Activities.Add(_mapper.Map<ActivityDetailsViewModel>(a)));

            model.Documents = new List<DocumentListViewModel>();
            var documents = _context.ModuleDocument.Where(d => d.ModuleId == id).ToList();
            foreach (var doc in documents)
            {
                var newDoc = _mapper.Map<DocumentListViewModel>(doc);
                newDoc.Filezise = (doc.DocumentData.Length).Bytes().Humanize("#.#");
                model.Documents.Add(newDoc);
            }

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

            var model = _mapper.Map<ModuleAddViewModel>(new Module());
            model.CourseId = course.Id;
            model.CourseName = course.Name;
            model.ParentStartDate = course.StartDate;
            model.ParentEndDate = course.EndDate;

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
        public async Task<IActionResult> Create([Bind("Name, Description, StartDate, EndDate, DocId, CourseId, ParentStartDate, ParentEndDate")] ModuleAddViewModel @module)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Module>(@module);
                _context.Add(model);

                await _context.SaveChangesAsync();
                TempData["AlertMsg"] = "Module added";
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
                    TempData["AlertMsg"] = "Module deleted";
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
                    var model = _mapper.Map<Module, ModuleAddViewModel>(module);
                    model.CourseId = course.Id;
                    model.CourseName = course.Name;
                    model.ParentStartDate = course.StartDate;
                    model.ParentEndDate = course.EndDate;
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
        public async Task<IActionResult> Edit([Bind("Id, Name, Description, StartDate, EndDate, CourseId, ParentStartDate, ParentEndDate")] ModuleAddViewModel @module)
        {
            if (ModelState.IsValid)
            {
                var moduleEntity = await _context.Modules.FirstOrDefaultAsync(a => a.Id == @module.Id);

                moduleEntity.Name = @module.Name;
                moduleEntity.StartDate = @module.StartDate;
                moduleEntity.EndDate = @module.EndDate;
                moduleEntity.Description = @module.Description;

                var activitiesOutSideStartEndDate = await GetActivitiesOutSideCourseStartEndDates(moduleEntity);
                if (activitiesOutSideStartEndDate.Count() > 0)
                {
                    var errorCount = 0;
                    foreach (var activity in activitiesOutSideStartEndDate)
                    {
                        ModelState.AddModelError($"activity_start_end_error_{errorCount++}", $"Activity: {activity.Description} {activity.StartDate.ToString(Common.DateFormat)} - {activity.EndDate.ToString(Common.DateFormat)} is outside module Start/End dates");
                    }
                    return View(@module);
                }

                _context.Update(moduleEntity);
                await _context.SaveChangesAsync();

                TempData["AlertMsg"] = "Saved changes";
                return RedirectToAction(nameof(Details), new { id = moduleEntity.Id });
            }

            return View(@module);
        }


        private async Task<List<Activityy>> GetActivitiesOutSideCourseStartEndDates(Module @module)
        {
            var res = new List<Activityy>();
            var activities = await _context.Activities.Where(a => a.ModuleId == @module.Id).ToListAsync();
            foreach (var activity in activities)
            {
                if (@module.StartDate.CompareTo(activity.StartDate) > 0 || @module.EndDate.CompareTo(activity.EndDate) < 0)
                {
                    res.Add(activity);
                }
            }
            return res;
        }
    }
}
