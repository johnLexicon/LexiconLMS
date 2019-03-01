using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiconLMS.Data;
using LexiconLMS.Models;
using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LexiconLMS.Controllers
{
    public class UserAdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserAdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: UserAdmin
        public ActionResult Index()
        {

            var vm = new UserAdminViewModel()
            {

            };


            return View();
        }

        // GET: UserAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAdminViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                Task<User> existingUser = _userManager.FindByEmailAsync(vm.Email);
                existingUser.Wait();

                if (existingUser.Result is null)
                {
                    User newUser = new User()
                    {
                        Email = vm.Email,
                        UserName = vm.Name,

                    };
                    Task<IdentityResult> createUser = _userManager.CreateAsync(newUser, vm.Password);
                    createUser.Wait();
                    //vm.Role = "Teacher";
                    if (createUser.Result.Succeeded)
                    {
                        Task<IdentityResult> addToRoleResult = _userManager.AddToRoleAsync(newUser, "Teacher");
                        addToRoleResult.Wait();
                    } else
                    {
                        ModelState.AddModelError("Name", "Invalid user name");
                        return View(vm);
                    }
                }
                return RedirectToAction(nameof(Index));

                //return View();
            }
            else
            {
                return View(vm);
            }
        }

        // GET: UserAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserAdmin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}