using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.BLL.Interfaces;
using Vezeeta.BLL.UnitOfWork;
using Vezeeta.DAL.Context;

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


            //builder.Services.AddIdentity<VezeetaDbContext, IdentityRole>(options =>
            //{
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 6;
            //})
            //.AddEntityFrameworkStores<VezeetaDbContext>()
            //.AddDefaultTokenProviders();

          //  builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          //.AddCookie(options =>
          //{
          //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
          //    options.SlidingExpiration = true;
          //    options.AccessDeniedPath = "/Forbidden/";
          //});

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<VezeetaDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Vezeeta") ?? throw new InvalidOperationException("Connection string 'Vezeeta' not found.")));

            var app = builder.Build();

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

            //app.UseAuthentication(); 
            //app.UseAuthorization();

            //app.MapRazorPages();

            //app.MapDefaultControllerRoute();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
