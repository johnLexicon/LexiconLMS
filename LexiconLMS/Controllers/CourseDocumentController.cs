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
    [Authorize(Roles ="Teacher")]
    public class CourseDocumentController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CourseDocumentController(LexiconLMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: CourseDocument/Create
        public ActionResult Create(int id)
        {
            var vm = new CreateDocumentViewModel()
            {
                EnitityId = id
            };

            return View("_CreateDocumentPartial", vm);
        }

        // POST: CourseDocument/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDocumentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newDocument = new CourseDocument()
                {
                    Description = vm.Description,
                    Name = vm.file.FileName,
                    UploadTime = DateTime.Now,
                    CourseId = vm.EnitityId,
                };

                newDocument.UserId = _userManager.GetUserId(User);

                using (var memoryStream = new MemoryStream())
                {
                    vm.file.CopyTo(memoryStream);
                    newDocument.DocumentData = memoryStream.ToArray();
                }

                _context.CourseDocument.Add(newDocument);
                _context.SaveChanges();

                //Can't get it to accept nameof(Details) for some reason
                return RedirectToAction("Details", nameof(Course), new { id = vm.EnitityId });
            }
            else
            {
                return View("_CreateDocumentPartial", vm);
            }
        }

        // GET: CourseDocument/Delete
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var document = _context.CourseDocument.FirstOrDefault(a => a.Id == id);
            if (!(document is null))
            {
                _context.Remove(document);
                _context.SaveChanges();

                var course = _context.Courses.FirstOrDefault(a => a.Id == document.CourseId);

                if (!(course is null))
                {
                    return RedirectToAction("Details", "Course", new { id = course.Id });
                }
            }

            return NotFound();
        }

        [Authorize(Roles ="Teacher, Student")]
        public ActionResult Display(int id)
        {
            var document = _context.CourseDocument.FirstOrDefault(d => d.Id == id);

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