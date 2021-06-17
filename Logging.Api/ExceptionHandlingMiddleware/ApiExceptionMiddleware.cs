using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Logging.Api.ExceptionHandlingMiddleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger, ApiExceptionOptions options)
        {
            _next = next;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, _options);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ApiExceptionOptions options)
        {
            var apiError = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
                Status = (int) HttpStatusCode.InternalServerError,
                Title = "Error occurred in the Api"
            };

            options.AddResponseDetails?.Invoke(context, exception, apiError);

            var innerMessage = GetInnerMostExceptionMessage(exception);

            _logger.LogError(exception, $"{innerMessage} -- {apiError.Id}");

            var result = JsonSerializer.Serialize(apiError);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }

        private string GetInnerMostExceptionMessage(Exception exception)
        {
            return exception.InnerException != null 
                ? GetInnerMostExceptionMessage(exception.InnerException) 
                : exception.Message;
        }
    }
}