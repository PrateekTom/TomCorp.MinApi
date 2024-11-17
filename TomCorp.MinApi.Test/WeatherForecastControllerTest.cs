using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TomCorp.MinApi.Controllers;

namespace TomCorp.MinApi.Test
{
    public class WeatherForecastControllerTest 
    {

        private readonly WeatherForecastController _weatherForecastController;

        public WeatherForecastControllerTest()
        {
            Mock<ILogger<WeatherForecastController>> mockLogger = new();
            _weatherForecastController = new WeatherForecastController(mockLogger.Object);
        }

        [Fact]
        public void Get_ReturnsWeatherForecasts_WithValidData()
        {
            // Act
            var result = _weatherForecastController.Get();

            // Assert
            Assert.IsType<WeatherForecast[]>(result);
            var weatherForecasts = result.ToArray();
            Assert.Equal(5, weatherForecasts.Length); // Default number of forecasts
            Assert.InRange(weatherForecasts[0].TemperatureC, -20, 55); // Temperature is within the expected range
            Assert.NotNull(weatherForecasts[0].Summary);
        }
        [Fact]
        public void Get_ReturnsWeatherForecasts_WithCorrectDate()
        {
            // Act
            var result = _weatherForecastController.Get();

            // Assert
            var weatherForecasts = result.ToArray();
            var firstForecastDate = weatherForecasts[0].Date;
            // The first forecast date should be tomorrow.
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), firstForecastDate);
        }
        [Fact]
        public void Get_ReturnsDifferentSummaries()
        {
            // Act
            var result = _weatherForecastController.Get();

            // Assert
            var weatherForecasts = result.ToArray();
            var summaries = weatherForecasts.Select(wf => wf.Summary).ToList();
            Assert.All(summaries, summary => Assert.Contains(summary, new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Hot", "Sweltering", "Scorching" }));
        }
    }
}