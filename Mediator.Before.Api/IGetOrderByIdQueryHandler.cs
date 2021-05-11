namespace Mediator.Before.Api
{
    public interface IGetOrderByIdQueryHandler
    {
        GetOrderByIdResponseModel MakeOrder(GetOrderByIdRequestModel model);
    }
}