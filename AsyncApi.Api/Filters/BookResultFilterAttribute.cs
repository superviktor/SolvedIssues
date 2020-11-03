using System.Threading.Tasks;
using AsyncApi.Api.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncApi.Api.Filters
{
    public class BookResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var actionResult = context.Result as ObjectResult;
            if (actionResult?.Value == null || actionResult.StatusCode < 200 || actionResult.StatusCode >= 300)
            {
                await next();
                return;
            }

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();

            actionResult.Value = mapper.Map<BookDto>(actionResult.Value);

            await next();
        }
    }
}