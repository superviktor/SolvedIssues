using ErrorHandlingMiddleware.Api.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ErrorHandlingMiddleware.Api.Middlewares
{
    public class ErrorHandlingMiddlewareComponent
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public ErrorHandlingMiddlewareComponent(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            var stackTrace = string.Empty;

            var exceptionType = exception.GetType();
            if (exceptionType == typeof(NotFoundException))
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(BadRequestException))
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                if (_environment.IsDevelopment())
                    stackTrace = exception.StackTrace;
            }

            var result = JsonSerializer.Serialize(new { error = exception.Message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
