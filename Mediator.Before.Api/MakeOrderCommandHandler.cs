using System;

namespace Mediator.Before.Api
{
    public class MakeOrderCommandHandler : IMakeOrderCommandHandler
    {
        public MakeOrderResponseModel MakeOrder(MakeOrderRequestModel model)
        {
            //logic here
            return new MakeOrderResponseModel
            {
                IsSuccess = true,
                OrderId = Guid.NewGuid()
            };
        }
    }
}