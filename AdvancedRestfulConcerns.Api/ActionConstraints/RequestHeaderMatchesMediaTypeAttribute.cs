using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace AdvancedRestfulConcerns.Api.ActionConstraints
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        private readonly string _requestHeaderToMatch;
        private readonly MediaTypeCollection _mediaTypes = new MediaTypeCollection();

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch, string mediaType, params string[] otherMediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch ?? throw new ArgumentNullException(nameof(requestHeaderToMatch));

            if (MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
                _mediaTypes.Add(parsedMediaType);
            else 
                throw new ArgumentException(nameof(mediaType));

            foreach (var otherMediaType in otherMediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(otherMediaType, out var parsedTOtherMediaType))
                    _mediaTypes.Add(parsedTOtherMediaType);
                else
                    throw new ArgumentException(nameof(otherMediaType));
            }
        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
                return false;

            var parsedRequestMediaType = new MediaType(requestHeaders[_requestHeaderToMatch]);
            return _mediaTypes.Select(mediaType => new MediaType(mediaType)).Contains(parsedRequestMediaType);
        }

        public int Order => 0;
    }
}
