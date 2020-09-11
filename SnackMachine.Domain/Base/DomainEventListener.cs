namespace SnackMachine.Domain.Base
{
    //this class should call DispatchEvents method when for example ORM commits transaction successfully
    public class DomainEventListener
    {
        public void OnCommitTransaction() // param is ORMCommitTransactionEvent ormEvent
        {
            AggregateRoot aggregateRoot = null; // ormEvent.Entity
            DispatchEvents(aggregateRoot);
        }

        private void DispatchEvents(AggregateRoot aggregateRoot)
        {
            foreach (var domainEvent in aggregateRoot.DomainEvents)
            {
                DomainEventsDispatcher.Dispatch(domainEvent);
            }

            aggregateRoot.ClearEvents();
        }
    }
}
