using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace MassTransitRabbitMq
{
    public static class QueueExtensions
    {
        public static IServiceCollection RegisterQueueServices(this IServiceCollection services)
        {
            services.AddSingleton(_ => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(RabbitMqConstants.HostName, RabbitMqConstants.VirtualHost,
                    h =>
                    {
                        h.Username(RabbitMqConstants.Username);
                        h.Password(RabbitMqConstants.Password);
                    });

                cfg.ExchangeType = ExchangeType.Direct;
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            return services;
        }
    }
}