using HelloLogging.WebApi.Services;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace HelloLogging.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Logger konfigurieren

        // Existierende Konfiguration entfernen
        builder.Logging.ClearProviders();

        // Install-Package OpenTelemetry.Instrumentation.AspNetCore
        // Install-Package OpenTelemetry.Exporter.Console
        builder.Logging.AddOpenTelemetry(options => options.AddConsoleExporter(options =>
        {
            // Hier koennen wir die Log-Einstellungen anpassen
        }));

        // Install-Package OpenTelemetry.Exporter.OpenTelemetryProtocol
        var otlpEndpoint = new Uri("http://localhost:5341/ingest/otlp/v1/logs");
        builder.Logging.AddOpenTelemetry(options => 
        {
            options.SetResourceBuilder(ResourceBuilder.CreateEmpty()
                .AddService(nameof(WeatherForecastService))
                .AddAttributes(new Dictionary<string, object> 
                { 
                    { "version", "1.0.123" },
                    { "environment", builder.Environment.EnvironmentName }
                }));
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
            options.AddOtlpExporter(config =>
            {
                config.Endpoint = otlpEndpoint;
                config.Protocol = OtlpExportProtocol.HttpProtobuf;
                config.Headers = "X-Seq-ApiKey=3CzFSmxsPHY4Bi6H41mh";
            });
        });

        #endregion

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
