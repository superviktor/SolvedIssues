using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ThreadSafeNumericIdGenerator.Api.Common
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                await HandlerExceptionAsync(context, exception);
            }
        }

        private Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            //log exception 
            string result = JsonSerializer.Serialize(Envelope.Error(exception.Message));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; ;

            return context.Response.WriteAsync(result);
        }
    }
}
