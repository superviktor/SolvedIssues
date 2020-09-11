using SnackMachine.Domain.Base;
using SnackMachine.Domain.SnackMachineBoundedContext;

namespace SnackMachine.Domain.MonitorBoundedContext
{
    public class SnackBoughtDomainEventHandler : IDomainEventHandler<SnackBought>
    {
        private readonly IMonitorRepository monitorRepository;

        public SnackBoughtDomainEventHandler(IMonitorRepository monitorRepository)
        {
            this.monitorRepository = monitorRepository;
        }

        public void Handle(SnackBought domainEvent)
        {
            var monitor = monitorRepository.GetMonitor();

            var operation = new BuyOperation(domainEvent.SlotPosition, domainEvent.SnackPileQuantityLeft, domainEvent.SnackName, domainEvent.DateTime);
            monitor.AddOperation(operation);
        }
    }
}
