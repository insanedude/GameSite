using System;
using System.Threading.Tasks;
using GameSiteProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GameSiteProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<GameSiteDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GameSiteConnectionString")));  // Ensure the correct name here

            builder.Services.AddDistributedMemoryCache(); // Required for storing session data in memory
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set timeout duration
                options.Cookie.HttpOnly = true; // Cookie settings for security
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.User.AllowedUserNameCharacters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+абвгґдеєжзиіїйклмнопрстуфхцчшщьюяёэъыАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯЁЭЪЫ";
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<GameSiteDbContext>()
                .AddDefaultTokenProviders();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
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

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Seeding db
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
                    RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    GameSiteDbContext context = services.GetRequiredService<GameSiteDbContext>();
                    await GameSiteSeedContext.SeedAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            app.Run();
        }
    }
}