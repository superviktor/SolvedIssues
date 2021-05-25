using System.Net;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Validation.Domain;

namespace Validation.Api
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        protected new IActionResult Ok(object result = null)
        {
            return new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.OK);
        }

        protected IActionResult Error(Error error, string invalidFiled = null)
        {
            return new EnvelopeResult(Envelope.Error(error, invalidFiled), HttpStatusCode.BadRequest);
        }

        protected IActionResult FromResult<T>(Result<T, Error> result)
        {
            if (result.IsSuccess)
                return Ok();

            return Error(result.Error);
        }
    }
}