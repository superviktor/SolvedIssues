using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Logging.Api.ExceptionHandlingMiddleware;
using Logging.Api.Logging;
using Logging.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Logging.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Logging.Api", Version = "v1" });
            });

            services.AddTransient<IWeatherService, WeatherService>();
            services.AddSingleton<IScopeInfo, ScopeInfo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiExceptionHandler(o =>
            {
                o.AddResponseDetails = UpdateApiErrorResponse;
                o.DeterminateLogLevel = DetermineLogLevel;
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logging.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private LogLevel DetermineLogLevel(Exception e)
        {
            return e.Message.Contains("database", StringComparison.InvariantCulture) 
                ? LogLevel.Critical 
                : LogLevel.Error;
        }

        private void UpdateApiErrorResponse(HttpContext context, Exception exception, ApiError apiError)
        {
            if (exception is IndexOutOfRangeException)
                apiError.Details = "Bla bla bla";
        }
    }
}
