using System.Collections.Generic;

namespace Logging.Api.DataAccess
{
    public interface IWeatherRepo
    {
        IEnumerable<WeatherForecast> Get();
    }
}
