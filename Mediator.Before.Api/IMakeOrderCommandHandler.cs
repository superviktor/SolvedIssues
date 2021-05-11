namespace Mediator.Before.Api
{
    public interface IMakeOrderCommandHandler
    {
        MakeOrderResponseModel MakeOrder(MakeOrderRequestModel model);
    }
}