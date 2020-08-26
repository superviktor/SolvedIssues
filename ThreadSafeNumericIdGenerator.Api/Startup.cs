using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThreadSafeNumericIdGenerator.Api.Common;
using ThreadSafeNumericIdGenerator.Application;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.AzureTablesRepository;
using ThreadSafeNumericIdGenerator.Domain.Repository;
using ThreadSafeNumericIdGenerator.Repository.DataContract;
using ThreadSafeNumericIdGenerator.Repository.Repository;

namespace ThreadSafeNumericIdGenerator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IAzureTableRepository<IdHolderTableEntity>>(s=> new AzureTableRepository<IdHolderTableEntity>(Configuration.GetConnectionString("AzureTables")));
            services.AddScoped<IIdHolderRepository, IdHolderRepository>();
            services.AddScoped<IIdHolderService, IdHolderService>();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Numeric Id Generator Api V1");
            });
            
            app.UseMiddleware<ExceptionHandler>();

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
