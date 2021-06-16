using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Logging.Api.DataAccess;

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
        public IActionResult Get([FromQuery] string city, [FromQuery] int days)
        {
            var scopeInfo = $" city={city}, days={days}";
            using var scope = _logger.BeginScope("WeatherForecast Get {scopeInfo}", scopeInfo);
            var weatherForecasts = _service.Get(city, days);
            return Ok(weatherForecasts);
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }
    }
}
