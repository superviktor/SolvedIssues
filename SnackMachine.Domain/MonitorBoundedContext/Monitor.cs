using System.Collections.Generic;
using SnackMachine.Domain.Base;

namespace SnackMachine.Domain.MonitorBoundedContext
{
    public class Monitor : AggregateRoot
    {
        private List<BuyOperation> operations = new List<BuyOperation>();

        public IReadOnlyCollection<BuyOperation> Operations => operations.AsReadOnly();

        public void AddOperation(BuyOperation buyOperation)
        {
            operations.Add(buyOperation);
        }
    }
}
