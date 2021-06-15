using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace ConsumerApp
{
    public class OrderWorker : BackgroundService
    {
        private readonly IBusControl _busControl;

        public OrderWorker(IBusControl busControl)
        {
            _busControl = busControl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var productChangeHandler = _busControl.ConnectReceiveEndpoint("order", x =>
            {
                x.Consumer<OrderMessageConsumer>();
            });

            await productChangeHandler.Ready;
        }
    }
}