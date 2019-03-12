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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ActivityController : Controller
    {

        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;

        public ActivityController(LexiconLMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }


        public async Task<IActionResult> Index()
        {
            var model = await _context.Activities.Include(a=>a.ActivityType).Include(a=>a.Module).Include(a => a.Module.Course).ToListAsync();
            return View(model);
        }

        // GET: Activity/Create
        public async Task<IActionResult> Create(int id)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var module = await _context.Modules.Include(m=>m.Course).FirstOrDefaultAsync(m => m.Id == id);

            if (module is null)
            {
                return NotFound();
            }

            var model = new ActivityViewModel();

            model.ModuleId = module.Id;
            model.ModuleName =module.Name;
            model.Module = module;
            model.Course = module.Course;

          
            var startTimeActivity = module.StartDate;
            model.StartDate = startTimeActivity;
           
            model.EndDate = startTimeActivity.AddDays(7);

            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Type");

            return View(model);
        }

        // POST: Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,StartDate,EndDate,ModuleId,ModuleName,ActivityTypeId")] ActivityViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
      
                Activityy activity = _mapper.Map<Activityy>(viewModel);
                activity.ActivityType = await _context.ActivityType.FirstOrDefaultAsync(at => at.Id == viewModel.ActivityTypeId);
                activity.Module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == viewModel.ModuleId);
                await _context.Activities.AddAsync(activity);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                // return RedirectToAction(nameof(Details), new { id =activity.Id});
                return RedirectToAction("Details", "module", new { id = activity.Module.Id });
            }
         
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Type");
            return View(viewModel);

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .Include(v => v.Module)
                .Include(v => v.ActivityType)
                .FirstOrDefaultAsync(m => m.Id== id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);


            if (activity == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ActivityViewModel>(activity);
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Type", activity.ActivityTypeId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,StartDate,EndDate,ModuleId,ModuleName,ActivityTypeId")] ActivityViewModel AVM)
        {

            if (ModelState.IsValid)
            {
                var ActivityEntity = await _context.Activities.Include(a =>a.ActivityType).FirstOrDefaultAsync(a => a.Id == AVM.Id);

                ActivityEntity.Description = AVM.Description;
                ActivityEntity.StartDate = AVM.StartDate;
                ActivityEntity.EndDate = AVM.EndDate;
                //ActivityEntity.ModuleId = AVM.ModuleId;
                ActivityEntity.ActivityTypeId = AVM.ActivityTypeId;
                _context.Update(ActivityEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = id });
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Type", AVM.ActivityTypeId);
            return View(AVM);
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FirstOrDefaultAsync(a => a.Id == id);
            if (!(activity is null))
            {
                _context.Remove(activity);
                _context.SaveChanges();

                var module = await _context.Modules.FirstOrDefaultAsync(a => a.Id == activity.ModuleId);
                if (!(module is null))
                {
                    return RedirectToAction("Details", "module", new { id = module.Id });
                }
            }

            return NotFound();
        }



    }
}
