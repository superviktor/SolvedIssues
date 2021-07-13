using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpObserveVersions.IntefaceDefaultMethods
{
    public interface ICustomer
    {
        IEnumerable<IOrder> PreviousOrders { get; }

        DateTime DateJoined { get; }
        DateTime? LastOrder { get; }
        string Name { get; }
        IDictionary<DateTime, string> Reminders { get; }

        public decimal ComputeLoyaltyDiscount() => DefaultLoyaltyDiscount(this);

        protected static decimal DefaultLoyaltyDiscount(ICustomer c)
        {
            var twoYearsAgo = DateTime.UtcNow.AddYears(-2);
            if (c.DateJoined < twoYearsAgo && c.PreviousOrders.Count() > 10)
                return 0.1m;
            return 0;
        }
    }
}