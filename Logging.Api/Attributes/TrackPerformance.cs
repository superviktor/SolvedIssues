using System;
using System.Diagnostics;
using Logging.Api.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Logging.Api.Attributes
{
    public class TrackPerformance : ActionFilterAttribute
    {
        private Stopwatch _timer;
        private readonly ILogger<TrackPerformance> _logger;
        private readonly IScopeInfo _scopeInfo;
        private IDisposable _hostScope;

        public TrackPerformance(ILogger<TrackPerformance> logger, IScopeInfo scopeInfo)
        {
            _logger = logger;
            _scopeInfo = scopeInfo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = new Stopwatch();
            _hostScope = _logger.BeginScope(_scopeInfo.HostScopeInfo);
            _timer.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            if(context.Exception is null)
                _logger.LogRoutePerformance(context.HttpContext.Request.Path, context.HttpContext.Request.Method, _timer.ElapsedMilliseconds);
            _hostScope?.Dispose();
        }
    }
}