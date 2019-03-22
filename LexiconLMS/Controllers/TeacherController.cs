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
using Microsoft.EntityFrameworkCore;

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
            var theUser = _userManager.GetUserAsync(User);
            theUser.Wait();

            var courses = _context.Courses.Where(c => c.StartDate <= today && c.EndDate > today);
            TeacherPageViewModel viewModel = new TeacherPageViewModel
            {
                OngoingCourses = courses.ToList()
            };

            var students = _userManager.GetUsersInRoleAsync("Student");
            students.Wait();

            var assignments = _context.ActivityDocument
                .Include(d => d.Activityy)
                .ThenInclude(e => e.Module)
                .ThenInclude(f => f.Course)
                .Where(d => students.Result.Contains(d.User))
                .Where(g => g.Activityy.Module.Course.Users.Contains(theUser.Result));

            viewModel.Assignments = _mapper.Map<List<ActivityDocument>, List<AssignmentListViewModel>>(assignments.ToList());

            return View(viewModel);
        }
    }
}