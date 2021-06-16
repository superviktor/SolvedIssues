using System.Collections.Generic;

namespace Logging.Api.DataAccess
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> Get(string city, int amountOfDays);
    }
}
