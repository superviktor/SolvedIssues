using System;
using System.Collections.Generic;

namespace EFCorePlusDDD.Api.Domain.Events
{
    public class EventDispatcher
    {
        private readonly MessageBus _messageBus;

        public EventDispatcher(MessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Dispatch(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                Dispatch(domainEvent);
            }
        }

        private void Dispatch(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case StudentEmailChangedEvent emailChangedEvent:
                    _messageBus.SendEmailChangedMessage(emailChangedEvent.StudentId, emailChangedEvent.Email);
                    break;
                default:
                    throw new Exception($"Unknown event type {domainEvent.GetType()}");
            }
        }
    }
}