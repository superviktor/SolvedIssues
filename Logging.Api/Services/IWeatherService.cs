using System.Collections.Generic;

namespace Logging.Api.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> Get(string city, int amountOfDays);
    }
}
