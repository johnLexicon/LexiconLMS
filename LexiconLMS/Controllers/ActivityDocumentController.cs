using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LexiconLMS.Data;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles ="Teacher, Student")]
    public class ActivityDocumentController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ActivityDocumentController(LexiconLMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }


        // GET: ActivityDocument/Create
        [Authorize(Roles ="Teacher")]
        public ActionResult Create(int id)
        {
            var vm = new CreateDocumentViewModel()
            {
                EnitityId = id
            };
            ViewData["Title"] = "Add Activity Document";
            ViewData["parentUrl"] = $"/Activity/Details/{id}"; 
            return View("_CreateDocumentPartial", vm);
        }

        // POST: ActivityDocument/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDocumentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if(vm.file is null)
                {
                    ModelState.AddModelError("file", "File can't be empty!");
                    ViewData["parentUrl"] = $"/Activity/Details/{vm.Id}";
                    ViewData["Title"] = "Add Activity Document";
                    return View("_CreateDocumentPartial", vm);
                }
                var newDocument = new ActivityDocument()
                {
                    Description = vm.Description,
                    Name = vm.file.FileName,
                    UploadTime = DateTime.Now,
                    ActivityId = vm.EnitityId,
                };
                if (newDocument.Description is null)
                {
                    newDocument.Description = "none";
                }
                newDocument.UserId = _userManager.GetUserId(User);

                using (var memoryStream = new MemoryStream())
                {
                    vm.file.CopyTo(memoryStream);
                    newDocument.DocumentData = memoryStream.ToArray();
                }

                _context.ActivityDocument.Add(newDocument);
                _context.SaveChanges();

                //Can't get it to accept nameof(Details) for some reason

                if (User.IsInRole("Teacher"))
                {
                    TempData["AlertMsg"] = "Document added";
                    return RedirectToAction("Details", "Activity", new { id = vm.EnitityId });
                } else
                {
                    TempData["AlertMsg"] = "Assignment uploaded - good luck!";
                    return RedirectToAction("Index", "Student");
                }
            }
            else
            {
                ViewData["parentUrl"] = $"/Activity/Details/{vm.Id}";
                ViewData["Title"] = "Add Activity Document";
                return View("_CreateDocumentPartial", vm);
            }
        }

        // GET: ActivityDocument/StudentCreate
        [Route("/Assignment/Create/{id}")]
        public ActionResult CreateStudent(int id)
        {
            var vm = new CreateDocumentViewModel()
            {
                EnitityId = id
            };
            var act = _context.Activities.FirstOrDefault(a => a.Id == id);
            ViewData["Title"] = $"Assignment deadline: {act.EndDate.ToShortDateString() }";
            ViewData["parentUrl"] = $"/";
            return View("_CreateDocumentPartial", vm);
        }

        [Authorize(Roles="Teacher")]
        // GET: CourseDocument/Delete
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var document = _context.ActivityDocument.FirstOrDefault(a => a.Id == id);
            if (!(document is null))
            {
                _context.Remove(document);
                _context.SaveChanges();

                var module = _context.Activities.FirstOrDefault(a => a.Id == document.ActivityId);

                if (!(module is null))
                {
                    TempData["AlertMsg"] = "Document deleted";
                    return RedirectToAction("Details", "Activity", new { id = module.Id });
                }
            }
            return NotFound();
        }

        public ActionResult Display(int id)
        {
            var document = _context.ActivityDocument.FirstOrDefault(d => d.Id == id);

            if (document is null)
            {
                return NotFound();
            }

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(document.Name, out contentType);
            if (contentType == "application/pdf")
            {
                //handle pdf:s separately
                return new FileStreamResult(new MemoryStream(document.DocumentData), contentType);
            }
            else
            {
                contentType = "application/force-download"; //Hackish, maybe not nessecary 
                return new FileStreamResult(new MemoryStream(document.DocumentData), contentType) { FileDownloadName = document.Name };
            }

        }
    }
}