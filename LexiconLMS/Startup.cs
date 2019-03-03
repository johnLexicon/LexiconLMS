using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LexiconLMS.Data;
using LexiconLMS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LexiconLMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(options =>
                {
                    options.Filters.Add(new ResponseCacheAttribute() { NoStore = true, Location = ResponseCacheLocation.None });
                }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<LexiconLMSContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LexiconLMSContext"))
            );

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<LexiconLMSContext>(); //The database context where to store the security info.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //Needs to be implemented before useMvc.
            //Middleware that handles the cookies used for the Core Identity.
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            CreateRoles(serviceProvider);
            CreateAdmin(serviceProvider);
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            string[] roles = { "Teacher", "Student" };
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                Task<bool> roleExistsTask = roleManager.RoleExistsAsync(role);
                roleExistsTask.Wait();

                //Create role If the role does not already exists.
                if (!roleExistsTask.Result)
                {
                    Task<IdentityResult> createRoleTask = roleManager.CreateAsync(new IdentityRole(role));
                    createRoleTask.Wait();
                }

            }
        }

        private void CreateAdmin(IServiceProvider serviceProvider)
        {

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


            var adminUserEmail = Configuration["LexiconLMS:TeacherMail"];
            Task<User> administrator = userManager.FindByEmailAsync(adminUserEmail);
            administrator.Wait();

            //Creates the administrator user if it does not already exists.
            if (administrator.Result is null)
            {
                User user = new User
                {
                    Email = adminUserEmail,
                    UserName = adminUserEmail
                };

                var teacherPw = Configuration["LexiconLMS:TeacherPW"];
                Task<IdentityResult> createAdmin = userManager.CreateAsync(user, teacherPw);
                createAdmin.Wait();

                //If the admin user was succesfully created it adds the Administrator role to the user.
                if (createAdmin.Result.Succeeded)
                {
                    Task<IdentityResult> addToRoleResult = userManager.AddToRoleAsync(user, "Teacher");
                    addToRoleResult.Wait();
                }

            }
        }
    }
}
