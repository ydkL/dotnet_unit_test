using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WeatherApi.Tests.Controllers
{
    public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<WeatherApi.Program>>
    {
        private readonly HttpClient _client;

        public WeatherForecastControllerTests(WebApplicationFactory<WeatherApi.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsSuccessStatusCodeAndWeatherData()
        {
            // Act
            var response = await _client.GetAsync("/Weather");

            // Assert
            response.EnsureSuccessStatusCode();
            var weather = await response.Content.ReadFromJsonAsync<Weather>();
            Assert.Equal(25, weather.Temperature);
            Assert.Equal("Sunny", weather.Condition);

            var response2 = await _client.GetAsync("/WeatherForecast");

            // Assert
            string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
            response2.EnsureSuccessStatusCode();
            var forecasts = await response2.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

            Assert.Equal(7, forecasts.Count());

            int index = 0;
            foreach (var forecast in forecasts)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.Equal(DateTime.Now.AddDays(index).Day, forecast.Date.Day);
                bool result = Summaries.Contains(forecast.Summary);
                Assert.True(result);

                index += 1;
            }
        }


        [Fact]
        public async Task GetWeatherForecast_ReturnsSuccessStatusCodeAndForecastData()
        {
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
            response.EnsureSuccessStatusCode();
            var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

            Assert.Equal(7, forecasts.Count());

            int index = 0;
            foreach (var forecast in forecasts)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.Equal(DateTime.Now.AddDays(index).Day, forecast.Date.Day);
                bool result = Summaries.Contains(forecast.Summary);
                Assert.True(result);
                index += 1;
            }
        }


        [Fact]
        public async Task GetWeatherForecast_ReturnsSuccessStatusCodeAndForecastDataRange()
        {
            var baseUrl = "/WeatherForecast/dateRange";
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);

            var url = $"{baseUrl}?startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}";

            var response = await _client.GetAsync(url);

            // Assert
            string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
            response.EnsureSuccessStatusCode();
            var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

            Assert.Equal(7, forecasts.Count());

            int index = 0;
            foreach (var forecast in forecasts)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.Equal(DateTime.Now.AddDays(index).Day, forecast.Date.Day);
                bool result = Summaries.Contains(forecast.Summary);
                Assert.True(result);
                index += 1;
            }
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsSuccessStatusCodeAndForecastCondition()
        {
            var baseUrl = "/WeatherForecast";
            string[] Summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

           for (int i = 0; i < Summaries.Length; i++)
            {
                var url = $"{baseUrl}?condition={Summaries[i]}";
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
                if (forecasts.Count() > 0)
                {
                    int index = 0;
                    foreach (var forecast in forecasts)
                    {
                        Assert.InRange(forecast.TemperatureC, -20, 55);
                        Assert.InRange(forecast.Date, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(7));
                        bool result = Summaries.Contains(forecast.Summary);
                        Assert.True(result);
                        index += 1;
                    }
                }
            }
        }
    }

    public class Weather
    {
        public int Temperature { get; set; }
        public string Condition { get; set; }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; set; }
    }
}
