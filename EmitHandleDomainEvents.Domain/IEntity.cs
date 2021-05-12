using System.Collections.Concurrent;

namespace EmitHandleDomainEvents.Domain
{
    public interface IEntity
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    }
}