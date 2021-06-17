using System;
using Microsoft.AspNetCore.Http;

namespace Logging.Api.ExceptionHandlingMiddleware
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    }
}