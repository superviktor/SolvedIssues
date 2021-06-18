using System;
using Logging.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Logging.Api.Services;

namespace Logging.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [TypeFilter(typeof(TrackPerformance))]
        public IActionResult Get([FromQuery] string city, [FromQuery] int days)
        {
            using var scope = _logger.BeginScope("WeatherForecast Get {scopeInfo}", $"city={city}, days={days}");
            var weatherForecasts = _service.Get(city, days);
            return Ok(weatherForecasts);
        }

        [HttpGet]
        [Route("error")]
        public IActionResult Error()
        {
            throw new Exception("Something with database");
        }
    }
}
