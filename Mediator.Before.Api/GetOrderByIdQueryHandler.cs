namespace Mediator.Before.Api
{
    public class GetOrderByIdQueryHandler : IGetOrderByIdQueryHandler
    {
        public GetOrderByIdResponseModel MakeOrder(GetOrderByIdRequestModel model)
        {
            //logic here
            return new GetOrderByIdResponseModel
            {
                ProductName = "Ball",
                Quantity = 1,
                UserName = "viktor"
            };
        }
    }
}