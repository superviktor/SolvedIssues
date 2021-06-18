using System;
using System.Collections.Generic;
using System.Linq;
using Logging.Api.Logging;
using Microsoft.Extensions.Logging;

namespace Logging.Api.Services
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string[] Cities = {
            "London", "Paris"
        };

        private const int MaxDays = 30;

        private readonly ILogger _logger;

        public WeatherService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Weather Service");
        }

        public IEnumerable<WeatherForecast> Get(string city, int amountOfDays)
        {
            if (!Cities.Contains(city))
            {
                _logger.LogWarning($"We have no forecast for {city}");
                return new List<WeatherForecast>();
            }

            if (amountOfDays <= MaxDays) 
                return WeatherForecasts(city, amountOfDays);

            _logger.LogInformation(WeatherServiceEvents.TooBigPeriod,$"User asked for too big period ({amountOfDays} days)");
            return WeatherForecasts(city, MaxDays);
        }

        private IEnumerable<WeatherForecast> WeatherForecasts(string city, int days)
        {
            var rng = new Random();
            return Enumerable.Range(1, days).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                    City = city
                })
                .ToArray();
        }
    }
}
