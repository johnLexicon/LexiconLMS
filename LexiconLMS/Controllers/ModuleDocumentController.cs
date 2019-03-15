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
    public class ModuleDocumentController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ModuleDocumentController(LexiconLMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Teacher")]
        // GET: ModuleDocument/Create
        public ActionResult Create(int id)
        {
            var vm = new CreateDocumentViewModel()
            {
                EnitityId = id
            };
            ViewData["Title"] = "Add Module Document";
            return View("_CreateDocumentPartial", vm);
        }

        [Authorize(Roles = "Teacher")]
        // POST: ModuleDocument/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDocumentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newDocument = new ModuleDocument()
                {
                    Description = vm.Description,
                    Name = vm.file.FileName,
                    UploadTime = DateTime.Now,
                    ModuleId = vm.EnitityId,
                };

                newDocument.UserId = _userManager.GetUserId(User);

                using (var memoryStream = new MemoryStream())
                {
                    vm.file.CopyTo(memoryStream);
                    newDocument.DocumentData = memoryStream.ToArray();
                }

                _context.ModuleDocument.Add(newDocument);
                _context.SaveChanges();

                TempData["AlertMsg"] = "Document added";
                //Can't get it to accept nameof(Details) for some reason
                return RedirectToAction("Details", nameof(Module), new { id = vm.EnitityId });
            }
            else
            {
                ViewData["Title"] = "Add Module Document";
                return View("_CreateDocumentPartial", vm);
            }
        }

        [Authorize(Roles = "Teacher")]
        // GET: CourseDocument/Delete
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var document = _context.ModuleDocument.FirstOrDefault(a => a.Id == id);
            if (!(document is null))
            {
                _context.Remove(document);
                _context.SaveChanges();

                var module = _context.Modules.FirstOrDefault(a => a.Id == document.ModuleId);

                if (!(module is null))
                {
                    TempData["AlertMsg"] = "Document deleted";
                    return RedirectToAction("Details", "Module", new { id = module.Id });
                }
            }

            return NotFound();
        }

        public ActionResult Display(int id)
        {
            var document = _context.ModuleDocument.FirstOrDefault(d => d.Id == id);

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