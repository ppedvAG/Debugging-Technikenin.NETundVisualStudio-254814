using Microsoft.EntityFrameworkCore;
using Northwind.WebApi.Models;
using Serilog;

namespace Northwind.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("northwind.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.

            builder.Services.AddControllers();
            
            var connectionString = builder.Configuration.GetConnectionString("Northwind");
            builder.Services.AddDbContext<NorthwindDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
