using Microsoft.Extensions.Logging;

namespace Logging.Api.Logging
{
    public class WeatherServiceEvents
    {
        public static EventId TooBigPeriod = new EventId(1, "TooBigPeriod");
    }
}
