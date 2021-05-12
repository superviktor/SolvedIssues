using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmitHandleDomainEvents.Domain
{
    public abstract class Entity : IEntity
    {
        [NotMapped]
        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new ConcurrentQueue<IDomainEvent>();

        [NotMapped]
        public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void Publish(IDomainEvent @event)
        {
            _domainEvents.Enqueue(@event);
        }

        protected Guid NewIdGuid()
        {
            return Guid.NewGuid();
        }
    }
}