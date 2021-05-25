﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Validation.Api
{
    public sealed class EnvelopeResult : IActionResult
    {
        private readonly Envelope _envelope;
        private readonly int _statusCode;

        public EnvelopeResult(Envelope envelope, HttpStatusCode statusCode)
        {
            _envelope = envelope;
            _statusCode = (int)statusCode;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_envelope)
            {
                StatusCode = _statusCode
            };

            return objectResult.ExecuteResultAsync(context);
        }
    }
}
