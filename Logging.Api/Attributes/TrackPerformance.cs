using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Logging.Api.Attributes
{
    public class TrackPerformance : ActionFilterAttribute
    {
        private Stopwatch _timer;
        private readonly ILogger<TrackPerformance> _logger;

        public TrackPerformance(ILogger<TrackPerformance> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = new Stopwatch();
            _timer.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            if(context.Exception is null)
                _logger.LogRoutePerformance(context.HttpContext.Request.Path, context.HttpContext.Request.Method, _timer.ElapsedMilliseconds);
        }
    }
}