using EmitHandleDomainEvents.Domain;
using MediatR;

namespace EmitHandleDomainEvents.Application
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}