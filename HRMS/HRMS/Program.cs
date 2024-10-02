using HRMS.Models; // Add this namespace for HRMSContext
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Add this namespace for cookie authentication

namespace HRMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Register HRMSContext with the connection string from appsettings.json
            builder.Services.AddDbContext<HRMSContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HRMSDatabase")));

            // Add services for controllers with views
            builder.Services.AddControllersWithViews();

            // Configure cookie authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/HR/Login"; // Set the login path for unauthorized access
                    options.AccessDeniedPath = "/HR/AccessDenied"; // Set the access denied path
                });

            // Add session services
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Make the cookie HttpOnly
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Enable authentication
            app.UseAuthorization();
            app.UseSession(); // Add this line to enable session management

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dashboard}/{action=Dashboard}/{id?}"); // Set the Dashboard as the default

            app.Run();
        }
    }
}
