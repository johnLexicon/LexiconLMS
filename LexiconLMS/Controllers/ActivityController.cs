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
            var model = await _context.Activities.Include(a=>a.ActivityType).ToListAsync();
            return View(model);
        }

        // GET: Activity/Create
        public async Task<IActionResult> Create(int id)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id == id);

            if (module is null)
            {
                return NotFound();
            }

            var model = new ActivityViewModel();

            model.ModuleId = module.Id;
            model.ModuleName =module.Name;

          
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
                // return RedirectToAction(nameof(Details), new { id = viewModel.Id });
                return RedirectToAction(nameof(Index));
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }









    }
}
