using System;
using GameSiteProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameSiteProject
{
    public class Program
    {
        public static void Main(string[] args)
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        // public static void Main(string[] args)
        // {
        //     
        //     var builder = WebApplication.CreateBuilder(args);
        //
        //
        //     builder.Services.AddDbContext<GameSiteDbContext>(options =>
        //         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        //     builder.Services.AddDistributedMemoryCache(); // Required for storing session data in memory
        //     builder.Services.AddSession(options =>
        //     {
        //         options.IdleTimeout = TimeSpan.FromMinutes(30); // Set timeout duration
        //         options.Cookie.HttpOnly = true; // Cookie settings for security
        //         options.Cookie.IsEssential = true; // Make the session cookie essential
        //     });
        //
        //     builder.Services.AddControllersWithViews();
        //
        //     var app = builder.Build();
        //
        //     if (app.Environment.IsDevelopment())
        //     {
        //         app.UseDeveloperExceptionPage();
        //     }
        //     else
        //     {
        //         app.UseExceptionHandler("/Home/Error");
        //         app.UseHsts();
        //     }
        //
        //     app.UseHttpsRedirection();
        //     app.UseStaticFiles();
        //
        //     app.UseSession();
        //
        //     app.UseRouting();
        //
        //     app.UseAuthorization();
        //
        //     app.MapControllerRoute(
        //         name: "default",
        //         pattern: "{controller=Home}/{action=Index}/{id?}");
        //
        //     app.Run();
        // }
    }
}