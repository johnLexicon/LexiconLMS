using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiconLMS.Data;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles ="Teacher")]
    public class UserAdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserAdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: UserAdmin
        public async Task<IActionResult> Index()
        {
            // TODO: currently returns ALL users, independent of role
            // Should use the RoleManager.UsersInRole
            
            return View(await _userManager.GetUsersInRoleAsync("Teacher"));
        }

        // GET: UserAdmin/Details/GUID
        public ActionResult Details(string id)
        {
            Task<User> theUser = _userManager.FindByIdAsync(id);
            theUser.Wait();
            var vm = new UserAdminViewModel()
            {
                Email = theUser.Result.Email,
                Name = theUser.Result.FullName,
                Id = theUser.Result.Id,
            };
            return View(vm);
        }

        // GET: UserAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAdminCreateViewModel vm)
        {
            if (vm.Password != vm.Password2)
            {
                ModelState.AddModelError("Password2", "The two passwords do not match!");
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            // Input seems valid, do go on
            Task<User> existingUser = _userManager.FindByEmailAsync(vm.Email);
            existingUser.Wait();

            if (existingUser.Result is null)
            {
                User newUser = new User()
                {
                    Email = vm.Email,
                    UserName = vm.Email,
                    FullName = vm.Name

                };
                Task<IdentityResult> createUser = _userManager.CreateAsync(newUser, vm.Password);
                createUser.Wait();
                if (createUser.Result.Succeeded)
                {
                    Task<IdentityResult> addToRoleResult = _userManager.AddToRoleAsync(newUser, "Teacher");
                    addToRoleResult.Wait();
                }
                else
                {
                    ModelState.AddModelError("Name", "Invalid user name");
                    return View(vm);
                }
            } else
            {
                ModelState.AddModelError("Email", "User/email already exists, not created");
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: UserAdmin/Edit/GUID
        public ActionResult Edit(string id)
        {
            var theUser = _userManager.FindByIdAsync(id);
            theUser.Wait();
            var vm = new UserAdminViewModel()
            {
                Id = theUser.Result.Id,
                Name = theUser.Result.FullName,
                Email = theUser.Result.Email
            };
            return View(vm);
        }

        // POST: UserAdmin/Edit/GUID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, [Bind("Id,Name,Password,Password2,Email")] UserAdminViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }
            if (vm.Password2 != vm.Password)
            {
                ModelState.AddModelError("Password2", "The two passwords do not match!");
                return View(vm);
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var theUser = _userManager.FindByIdAsync(vm.Id);
            theUser.Wait();
            if (theUser.Result is null)
            {
                return NotFound();
            }

            var token = _userManager.GeneratePasswordResetTokenAsync(theUser.Result);
            token.Wait();

            theUser.Result.FullName = vm.Name;
            theUser.Result.Email = vm.Email;
            theUser.Result.UserName = vm.Email;
            _userManager.UpdateAsync(theUser.Result).Wait();

            var result = _userManager.ResetPasswordAsync(theUser.Result, token.Result, vm.Password);
            result.Wait();

            if (result.Result.Succeeded)
            {
                return View("Details", vm);
            }
            else
            {
                ModelState.AddModelError("Password", "Invalid password");
                return View(vm);
            }
        }

        // GET: UserAdmin/Delete/GUID
        public ActionResult Delete(string id)
        {
            Task<User> theUser = _userManager.FindByIdAsync(id);
            theUser.Wait();
            var vm = new UserAdminViewModel()
            {
                Email = theUser.Result.Email,
                Name = theUser.Result.FullName,
                Id = theUser.Result.Id,
            };
            return View(vm);
        }

        // POST: UserAdmin/Delete/GUID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Task<User> theUser = _userManager.FindByIdAsync(id);
            theUser.Wait();
            var deleteUser = _userManager.DeleteAsync(theUser.Result);
            deleteUser.Wait();
            return RedirectToAction(nameof(Index));
        }
    }
}