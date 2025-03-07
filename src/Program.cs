using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SweetDebt.Contexts;
using SweetDebt.Models;
using SweetDebt.Service;
using System;

namespace SweetDebt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages(options => options.Conventions.AllowAnonymousToPage("/Privacy").AllowAnonymousToPage("/Error"));
            builder.Services.AddDbContext<SweetDebtContext>(options => options.UseSqlite("Data Source=Data.db"));
            builder.Services.AddScoped<TransactionsService>();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            builder.Services.AddAuthorization();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SweetDebtContext>();
                dbContext.Database.Migrate();
                var dbService = scope.ServiceProvider.GetRequiredService<LoginService>();
                dbService.RegisterUserAsync("admin", "test", true);

            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseRouting();
            app.Use(async (context, next) => //testing
            {
                Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
                await next();
            });
            app.UseAuthorization();


            app.MapRazorPages().RequireAuthorization();


            app.Run();
        }
    }
}
