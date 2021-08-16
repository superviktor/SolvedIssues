using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisDistributedCache.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        public WeatherForecastController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public string CachedTimeUtc { get; set; }

        [HttpPost]
        public async Task Set()
        {
            var currentTimeUtc = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            var encodedCurrentTimeUtc = Encoding.UTF8.GetBytes(currentTimeUtc);
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
            await _distributedCache.SetAsync("key", encodedCurrentTimeUtc, options);
        }

        [HttpGet]
        public async Task<string> Get()
        {
            CachedTimeUtc = "Cached Time Expired";
            var encodedCachedTimeUtc = await _distributedCache.GetAsync("key");
            if (encodedCachedTimeUtc != null)
                CachedTimeUtc = Encoding.UTF8.GetString(encodedCachedTimeUtc);
            return CachedTimeUtc;
        }
    }
}
