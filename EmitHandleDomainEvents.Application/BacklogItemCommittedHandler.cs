using System.Threading;
using System.Threading.Tasks;
using EmitHandleDomainEvents.Domain;
using EmitHandleDomainEvents.Persistence;
using MediatR;

namespace EmitHandleDomainEvents.Application
{
    public class BacklogItemCommittedHandler : INotificationHandler<DomainEventNotification<BacklogItemCommitted>>
    {
        private readonly ApplicationDbContext dbContext;

        public BacklogItemCommittedHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Handle(DomainEventNotification<BacklogItemCommitted> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            //logic here
            return Task.CompletedTask;
        }
    }
}