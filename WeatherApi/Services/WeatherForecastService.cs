using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            var rng = new Random();
            var forecasts = new List<WeatherForecast>();

            for (int i = 0; i < 7; i++)
            {
                forecasts.Add(new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                });
            }

            return forecasts;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}