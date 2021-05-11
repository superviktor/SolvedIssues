using System.Threading;
using System.Threading.Tasks;
using Mediator.Before.Api;
using MediatR;

namespace Mediator.After.Api
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdRequestModel, GetOrderByIdResponseModel>
    {
        public Task<GetOrderByIdResponseModel> Handle(GetOrderByIdRequestModel request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetOrderByIdResponseModel
            {
                ProductName = "Ball",
                Quantity = 1,
                UserName = "viktor"
            });
        }
    }
}