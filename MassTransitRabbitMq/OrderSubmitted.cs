using System;
using MassTransitRabbitMqShared;

namespace MassTransitRabbitMq
{
    public class OrderSubmitted : IOrderMessage
    {
        public OrderSubmitted(string productName, int amount)
        {
            ProductName = productName;
            Amount = amount;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}