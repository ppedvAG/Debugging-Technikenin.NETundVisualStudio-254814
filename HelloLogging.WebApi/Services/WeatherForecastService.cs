namespace HelloLogging.WebApi.Services;

// Best Practices: Interface fuer diesen Service erstellen
public class WeatherForecastService : IWeatherForecastService
{
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public WeatherForecast[] GetWeatherForecast(int days, string? city)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, days).Select(index =>
            new WeatherForecast
            {
                City = city,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })
            .ToArray();

        // Wichtig: Statt String-Interpolation besser Parameters explizit uebergeben
        // Vorteil: Bei spaeterer Auswertung der Logs koennen wir die Daten besser verarbeiten
        _logger.LogInformation("Retrieved {WeatherCount} weather forecast for {City} on {Date}", forecast.Length, city, forecast[0].Date);

        return forecast;
    }
}