using System;

namespace MassTransitRabbitMqShared
{
    public interface IOrderMessage
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}