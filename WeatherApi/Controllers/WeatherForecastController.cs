using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.Services;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _forecastService;

    public WeatherForecastController(IWeatherForecastService forecastService)
    {
        _forecastService = forecastService;
    }


    [HttpGet]
    public ActionResult<IEnumerable<WeatherForecast>> Get()
    {
        var forecasts = _forecastService.GetWeatherForecasts();
        return Ok(forecasts);
    }

    [HttpGet("dateRange")]
    public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecastsByDateRange(DateTime startDate, DateTime endDate)
    {
        var forecasts = _forecastService.GetWeatherForecasts()
            .Where(forecast => forecast.Date >= startDate && forecast.Date <= endDate)
            .ToList();
        return Ok(forecasts);
    }

    [HttpGet("condition")]
    public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecastsByCondition(string condition)
    {
        var forecasts = _forecastService.GetWeatherForecasts()
            .Where(forecast => forecast.Summary.ToLower() == condition.ToLower())
            .ToList();
        return Ok(forecasts);
    }

    [HttpGet("nextWeek")]
    public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecastsForNextWeek()
    {
        var today = DateTime.Today;
        var endDate = today.AddDays(7);
        var forecasts = _forecastService.GetWeatherForecasts()
            .Where(forecast => forecast.Date >= today && forecast.Date <= endDate)
            .ToList();
        return Ok(forecasts);
    }

    [HttpGet("temperatureRange")]
    public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecastsByTemperatureRange(int minTemp, int maxTemp)
    {
        var forecasts = _forecastService.GetWeatherForecasts()
            .Where(forecast => forecast.TemperatureC >= minTemp && forecast.TemperatureC <= maxTemp)
            .ToList();
        return Ok(forecasts);
    }

    [HttpGet("dateAndCondition")]
    public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecastsByDateAndCondition(DateTime date, string condition)
    {
        var forecasts = _forecastService.GetWeatherForecasts()
            .Where(forecast => forecast.Date.Date == date.Date && forecast.Summary.ToLower() == condition.ToLower())
            .ToList();
        return Ok(forecasts);
    }

    [HttpGet("nextNDays")]
    public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecastsForNextNDays(int days)
    {
        var today = DateTime.Today;
        var endDate = today.AddDays(days);
        var forecasts = _forecastService.GetWeatherForecasts()
            .Where(forecast => forecast.Date >= today && forecast.Date <= endDate)
            .ToList();
        return Ok(forecasts);
    }


    [HttpGet("location")]
    public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecastsByLocation(string city, string country)
    {
        // You can implement logic here to fetch forecasts based on location (e.g., using a weather API)
        // For demonstration, let's return mock data
        var forecasts = new List<WeatherForecast>
    {
        new WeatherForecast { Date = DateTime.Today, TemperatureC = 25, Summary = "Sunny" },
        new WeatherForecast { Date = DateTime.Today.AddDays(1), TemperatureC = 23, Summary = "Cloudy" },
        new WeatherForecast { Date = DateTime.Today.AddDays(2), TemperatureC = 20, Summary = "Rainy" },
        // Add more mock data as needed
    };
        return Ok(forecasts);
    }



}

