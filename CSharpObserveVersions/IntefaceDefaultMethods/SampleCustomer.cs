using System;
using System.Collections.Generic;

namespace CSharpObserveVersions.IntefaceDefaultMethods
{
    public class SampleCustomer : ICustomer
    {
        public SampleCustomer(DateTime dateJoined, string name)
        {
            DateJoined = dateJoined;
            Name = name;
        }

        public IEnumerable<IOrder> PreviousOrders { get; }
        public DateTime DateJoined { get; }
        public DateTime? LastOrder { get; }
        public string Name { get; }
        public IDictionary<DateTime, string> Reminders { get; }
    }
}