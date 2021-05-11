using System;

namespace Mediator.Before.Api
{
    public class MakeOrderRequestModel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}