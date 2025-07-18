using Microsoft.EntityFrameworkCore;
using Northwind.Blazor.Components;
using Northwind.WebApi.Models;

namespace Northwind.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Microsoft empfiehlt die DbContextFactory zu nutzen, da Razor-Komponenten
            // potentiell parallel ausgeführt werden und ein Scoped-Service wie DbContext
            // dann zu Fehlern führen kann.
            var connectionString = builder.Configuration.GetConnectionString("Northwind");
            builder.Services.AddDbContextFactory<NorthwindDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
