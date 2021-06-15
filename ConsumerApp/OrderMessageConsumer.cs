using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;
using MassTransitRabbitMqShared;

namespace ConsumerApp
{
    public class OrderMessageConsumer : IConsumer<IOrderMessage>
    {
        public Task Consume(ConsumeContext<IOrderMessage> context)
        {
            Debug.WriteLine($"*********Message id: {context.Message.Id}*********");
            return Task.CompletedTask;
        }
    }
}