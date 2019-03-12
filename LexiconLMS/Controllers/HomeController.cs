using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public HomeController(IMapper mapper,SignInManager<User> signInManager,UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if(user is null)
                {
                    return View();
                }

                var userRole = _userManager.GetRolesAsync(user).Result.Single();
                if (userRole == "Teacher")
                {
                    return RedirectToAction("Index", "Teacher");
                }
                else if(userRole == "Student")
                {
                    return RedirectToAction("Index", "Student");
                }
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LogInViewModel LVM)
        {
            if (!ModelState.IsValid)
            {
                return View(LVM);

            }

            var user = await _userManager.FindByNameAsync(LVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User Name Not Found!");
                return View(LVM);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, LVM.Password, isPersistent:LVM.RememberMe, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Wrong Password!");
                return View(LVM);
            }

            var role = _userManager.GetRolesAsync(user).Result.Single();
            if (role=="Teacher")
            {
              return   RedirectToAction("Index","Teacher");
            } else if (role == "Student")
            {
                return RedirectToAction("Index", "Student");
            }
            else
            {
                return NotFound();
            }

        }


        public async Task<IActionResult> Logout()
        {
           await  _signInManager.SignOutAsync();
            // return Redirect("http://www.lexicon.se");
            return RedirectToAction("Index", "Home");

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
