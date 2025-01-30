using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SweetDebt.Contexts;
using SweetDebt.Models;
using SweetDebt.Service;

namespace SweetDebt
{
    public class Program
    {
        //TODO: Service for handle and store username and password
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<SweetDebtContext>(options => options.UseSqlite("Data Source=Data.db"));
            builder.Services.AddScoped<SweetDebtService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            builder.Services.AddAuthorization();



            var app = builder.Build();

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

            app.UseAuthorization();

            app.MapRazorPages().RequireAuthorization();
            app.Use(async (context, next) => //testing
            {
                Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
                await next();
            });

            app.Run();
        }
    }
}
