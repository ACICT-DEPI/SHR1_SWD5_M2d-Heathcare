using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.BLL.Interfaces;
using Vezeeta.BLL.UnitOfWork;
using Vezeeta.DAL.Context;
using Vezeeta.DAL.Entities;

namespace Vezeeta.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Set password options (customize as needed)
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                // Disable email confirmation requirement
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true; // Ensure unique email addresses
            })
            .AddEntityFrameworkStores<VezeetaDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
              options.SlidingExpiration = true;
              options.AccessDeniedPath = "/Forbidden/";
          });

            builder.Services.AddDbContext<VezeetaDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Vezeeta") ?? throw new InvalidOperationException("Connection string 'Vezeeta' not found.")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



            var app = builder.Build();

            // Seed roles here
            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //    SeedRoles(roleManager).Wait();
            //}


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapRazorPages();
            app.MapDefaultControllerRoute();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}");

            app.Run();
        }

        //private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        //{
        //    // List of roles to seed
        //    var roles = new[] { "Admin", "Doctor", "Patient" };

        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(role));
        //        }
        //    }
        //}
    }
}
