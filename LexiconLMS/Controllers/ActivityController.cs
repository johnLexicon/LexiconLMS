using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
       

        public ActivityController(LexiconLMSContext context)
        {
            _context = context;
           
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

            var dateTimeNow = DateTime.Now;
            model.StartDate = dateTimeNow;
            model.EndDate = dateTimeNow.AddDays(7);

            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Type");

            return View(model);
        }

        // POST: Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,StartDate,EndDate,ModuleId,ModuleName,ActivityType")] ActivityViewModel @activity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@activity);
                await _context.SaveChangesAsync();
              //  return RedirectToAction(nameof(Details), new { id = @activity.Id });
            }
         
            ViewData["ActivityTypeId"] = new SelectList(_context.Set<ActivityType>(), "Id", "Type", @activity.ActivityType);
            return View(@activity);

        }










    }
}
