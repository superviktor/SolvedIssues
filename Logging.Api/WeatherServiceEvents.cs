using Microsoft.Extensions.Logging;

namespace Logging.Api
{
    public class WeatherServiceEvents
    {
        public static EventId TooBigPeriod = new EventId(1, "TooBigPeriod");
    }
}
