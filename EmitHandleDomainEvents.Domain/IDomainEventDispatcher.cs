using System.Threading.Tasks;

namespace EmitHandleDomainEvents.Domain
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent @event);
    }
}