using System;
using Mediator.Before.Api;
using MediatR;

namespace Mediator.After.Api 
{
    public class MakeOrderRequestModel : IRequest<MakeOrderResponseModel>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}