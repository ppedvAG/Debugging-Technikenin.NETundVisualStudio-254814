namespace HelloLogging.WebApi.Services
{
    public interface IWeatherForecastService
    {
        WeatherForecast[] GetWeatherForecast(int days, string? city);
    }
}