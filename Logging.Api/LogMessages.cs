using System;
using Microsoft.Extensions.Logging;

namespace Logging.Api
{
    public static class LogMessages
    {
        private static readonly Action<ILogger, string, string, long, Exception> RoutePerformance;

        static LogMessages()
        {
            RoutePerformance = LoggerMessage.Define<string, string, long>(LogLevel.Information, 0,
                "{RouteName} {Method} code took {ElapsedMilliseconds}.");
        }

        public static void LogRoutePerformance(this ILogger logger, string route, string method,
            long elapsedMilliseconds)
        {
            RoutePerformance(logger, route, method, elapsedMilliseconds, null);
        }
    }
}