using RailwayOrientedProgramming.Common;
using RailwayOrientedProgramming.Domain;

namespace RailwayOrientedProgramming.Application
{
    public interface IThirdPartyPaymentServiceAfter
    {
        Result ChargePayment(BillingInfo billingInfo, MoneyAmount moneyAmount);
        void RollbackLastTransaction();
    }
}
