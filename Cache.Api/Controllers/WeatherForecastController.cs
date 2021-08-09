using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMemoryCache _memoryCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            //1 option
            //if (_memoryCache.TryGetValue(city, out IEnumerable<WeatherForecast> cacheForecast)) return cacheForecast;

            //var rng = new Random();
            //cacheForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //    {
            //        Date = DateTime.Now.AddDays(index),
            //        TemperatureC = rng.Next(-20, 55),
            //        Summary = Summaries[rng.Next(Summaries.Length)]
            //    })
            //    .ToArray();
            //var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            //_memoryCache.Set(city, cacheForecast, cacheOptions);

            //2 option
            var cacheForecast = await _memoryCache.GetOrCreateAsync(city, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                entry.SetSize(1024);
                var rng = new Random();
                return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                    .ToArray());
            });

            return Ok(cacheForecast);
        }
    }
}
