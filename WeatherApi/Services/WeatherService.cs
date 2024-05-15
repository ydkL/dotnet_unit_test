namespace WeatherApi.Services
{
    public interface IWeatherService
    {
        Weather GetWeather();
    }

    public class WeatherService : IWeatherService
    {
        public Weather GetWeather()
        {
            return new Weather { Temperature = 25, Condition = "Sunny" };
        }
    }

    public class Weather
    {
        public int Temperature { get; set; }
        public string Condition { get; set; }
    }
}
