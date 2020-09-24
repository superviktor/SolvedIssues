using AdvancedRestfulConcerns.Api.Helpers;
using AdvancedRestfulConcerns.Api.Persistence;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedRestfulConcerns.Api
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
            services.AddHttpCacheHeaders(
                expiration =>
            {
                expiration.MaxAge = 60;
                expiration.CacheLocation = CacheLocation.Private;
            }, validation =>
                {
                    validation.MustRevalidate = true;
                }
            );
            //services.AddResponseCaching();
            services.AddControllers(s =>
            {
                s.ReturnHttpNotAcceptable = true;
                s.CacheProfiles.Add("240SecsCacheProfile", new CacheProfile { Duration = 240 });
            });
            services.AddSwaggerGen();

            services.AddTransient<IResourceRepository, ResourcesRepository>();
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Advanced RESTful concerns Api V1");
            });

            app.UseHttpCacheHeaders();
            app.UseResponseCaching();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
