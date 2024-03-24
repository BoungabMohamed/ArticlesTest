using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repository;
using BuiesnesLogic.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Trach.Utility;
namespace Trach
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<appDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("appDbContextConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<appDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            // Add Razor Pages services.
            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // This is needed to enable authentication.
            app.UseAuthorization();
            app.UseStaticFiles();

            // Map Razor Pages.
            app.MapRazorPages();

            // Map controller routes.
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Identity}/{controller=Redirect}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
