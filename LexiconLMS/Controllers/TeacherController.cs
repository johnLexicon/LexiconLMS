using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LexiconLMS.Data;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly LexiconLMSContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public TeacherController(LexiconLMSContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var today = DateTime.Now;

            var courses = _context.Courses.Where(c => c.StartDate <= today && c.EndDate >= today);
            TeacherPageViewModel viewModel = new TeacherPageViewModel
            {
                Courses = courses.ToList()
            };

            return View(viewModel);
        }
    }
}