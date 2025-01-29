using Microsoft.EntityFrameworkCore;
using SweetDebt.Contexts;
using SweetDebt.Models;
using SweetDebt.Service;

namespace SweetDebt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<SweetDebtContext>(options => options.UseSqlite("Data Source=Data.db"));
            builder.Services.AddScoped<SweetDebtService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.Use(async (context, next) => //testing
            {
                Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
                await next();
            });

            app.Run();
        }
    }
}
