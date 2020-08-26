using RailwayOrientedProgramming.Domain;

namespace RailwayOrientedProgramming.Application
{
    public interface IThirdPartyPaymentServiceBefore
    {
        void ChargePayment(BillingInfo billingInfo, decimal moneyAmount);
        void RollbackLastTransaction();
    }
}
