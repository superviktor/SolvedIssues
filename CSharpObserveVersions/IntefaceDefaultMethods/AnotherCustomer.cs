using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpObserveVersions.IntefaceDefaultMethods
{
    public class AnotherCustomer :  ICustomer 
    {
        public AnotherCustomer(DateTime dateJoined, string name, IEnumerable<IOrder> previousOrders)
        {
            DateJoined = dateJoined;
            Name = name;
            PreviousOrders = previousOrders;
        }

        public IEnumerable<IOrder> PreviousOrders { get; }
        public DateTime DateJoined { get; }
        public DateTime? LastOrder { get; }
        public string Name { get; }
        public IDictionary<DateTime, string> Reminders { get; }

        public decimal ComputeLoyaltyDiscount()
        {
            if (PreviousOrders.Any() == false)
                return 0.50m;
            return ICustomer.DefaultLoyaltyDiscount(this);
        }
    }
}