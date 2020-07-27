using CqrsTemplate.Application;
using CqrsTemplate.Application.Common;
using CqrsTemplate.Application.QueryHandlers;
using CqrsTemplate.DataContracts;
using CqrsTemplate.Domain.CommandHandlers;
using CqrsTemplate.Domain.Commands;
using CqrsTemplate.Domain.Common;
using CqrsTemplate.Domain.Queries;
using CqrsTemplate.Domain.Repository;
using CqrsTemplate.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CqrsTemplate.Api
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
            services.AddTransient<IModelRepository, ModelRepository>();
            services.AddTransient<ICommandHandler<UpdateModelCommand>, UpdateModelCommandHandler>(); 
            services.AddTransient<IQueryHandler<GetAllModelsQuery, IEnumerable<ModelDto>>, GetAllModelsQueryHandler>();
            services.AddSingleton<MessagesDispatcher>();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cqrs template");
            });

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
