using HelloLogging.WebApi.Services;

namespace HelloLogging.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // WeahterForecastService bei Programmstart registrieren
        builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/weatherforecast", (IWeatherForecastService service, int days, string? city) =>
        {
            return service.GetWeatherForecast(days, city);
        });

        app.Run();
    }
}
