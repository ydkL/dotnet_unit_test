using Xunit;
using FluentAssertions;
using WeatherApi.Services;
using System;
using System.Collections.Generic;
using WeatherApi.Models;
using System.Linq;

public class WeatherServiceTests
{
    [Fact]
    public void GetWeather_ShouldReturnWeatherData()
    {
        // Arrange
        var weatherService = new WeatherService();

        // Act
        var result = weatherService.GetWeather();

        // Assert
        result.Temperature.Should().Be(25);
        result.Condition.Should().Be("Sunny");

        var weatherService2 = new WeatherForecastService();
        var result2 = weatherService2.GetWeatherForecasts();

        

        var forecasts = weatherService2.GetWeatherForecasts();

        forecasts.Count().Should().Be(7);

        int index = 0;
        foreach(var forecast in forecasts)
        {
            forecast.TemperatureC.Should().BeInRange(-20, 55);
            forecast.Date.Day.Should().Be(DateTime.Now.AddDays(index).Day);
            forecast.Summary.Should().BeOneOf(Summaries);
            index += 1;
        }
    }

    private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
}
