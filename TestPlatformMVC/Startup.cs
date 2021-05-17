using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestPlatform.DAL.Identity;
using TestPlatform.DAL;
using System;
using System.Threading.Tasks;
using TestPlatform.DAL.Enums;
using TestPlatform.BLL.Services;
using TestPlatfom.BLL.Logic;
using TestPlatform.DAL.Repos;

namespace TestPlatformMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<TestPlatformDBContext>
                (options => {
                    options.UseSqlServer(Configuration.GetConnectionString("Default"));
                });
            services
                .AddIdentity<User, IdentityRole>(options => {

                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<TestPlatformDBContext>();
            services.AddMvc().AddRazorRuntimeCompilation();

            services.AddScoped<ICoursesRepo, CoursesRepo>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ISignInUpOutLogic, SignInUpOutLogic>();
            services.AddScoped<ISuperAdminLogic, SuperAdminLogic>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateRoles(services).Wait();
        }
        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


            if (!await roleManager.RoleExistsAsync(Roles.SuperAdmin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Mentor.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Mentor.ToString()));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Lector.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Lector.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Student.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
            }


            var user = new User
            {
                Name = "SuperAdmin",
                SurName = "SuperAdmin",
                UserName = "SuperAdmin",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
            };

            const string password = "Test1234!";

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
            }

            user = new User
            {
                Name = "Admin",
                SurName = "Admin",
                UserName = "Admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

            user = new User
            {
                Name = "Mentor",
                SurName = "Mentor",
                UserName = "Mentor",
                Email = "mentor@gmail.com",
                EmailConfirmed = true,
            };

            result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Mentor.ToString());
            }

            user = new User
            {
                Name = "Lector",
                SurName = "Lector",
                UserName = "Lector",
                Email = "Lector@gmail.com",
                EmailConfirmed = true,
            };

            result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Lector.ToString());
            }

            user = new User
            {
                Name = "Student",
                SurName = "Student",
                UserName = "Student",
                Email = "Student@gmail.com",
                EmailConfirmed = true,
            };

            result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Student.ToString());
            }
        }

    }
}
