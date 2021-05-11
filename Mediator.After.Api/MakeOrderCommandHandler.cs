using System;
using System.Threading;
using System.Threading.Tasks;
using Mediator.Before.Api;
using MediatR;

namespace Mediator.After.Api
{
    public class MakeOrderCommandHandler : IRequestHandler<MakeOrderRequestModel, MakeOrderResponseModel>
    {
        public Task<MakeOrderResponseModel> Handle(MakeOrderRequestModel request, CancellationToken cancellationToken)
        {
            //logic here
            return Task.FromResult(new MakeOrderResponseModel
            {
                IsSuccess = true,
                OrderId = Guid.NewGuid()
            });
        }
    }
}