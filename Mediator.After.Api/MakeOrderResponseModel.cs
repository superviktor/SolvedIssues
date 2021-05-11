using System;

namespace Mediator.Before.Api
{
    public class MakeOrderResponseModel
    {
        public Guid OrderId { get; set; }
        public bool IsSuccess { get; set; }
    }
}