using MassTransit;
using MassTransitRabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace ConsumerApp
{
    public static class QueueExtension
    {
        public static IServiceCollection RegisterQueueServices(this IServiceCollection services)
        {
            services.AddMassTransit(c =>
            {
                c.AddConsumer<OrderMessageConsumer>();
            });

            services.AddSingleton(_ => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(RabbitMqConstants.HostName, RabbitMqConstants.VirtualHost,
                    h =>
                    {
                        h.Username(RabbitMqConstants.Username);
                        h.Password(RabbitMqConstants.Password);
                    });
            }));

            return services;
        }
    }
}