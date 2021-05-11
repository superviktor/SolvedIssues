using System;
using Mediator.Before.Api;
using MediatR;

namespace Mediator.After.Api
{
    public class GetOrderByIdRequestModel : IRequest<GetOrderByIdResponseModel>
    {
        public Guid Id { get; set; }
    }
}