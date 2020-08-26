using RailwayOrientedProgramming.Common;
using RailwayOrientedProgramming.Domain;

namespace RailwayOrientedProgramming.Application
{
    public class CustomerServiceAfter
    {
        private readonly ILogger logger;
        private readonly ICustomerRepositoryAfter customerRepositoryAfterAfter;
        private readonly IThirdPartyPaymentServiceAfter paymentServiceAfter;
        public CustomerServiceAfter(ILogger logger, ICustomerRepositoryAfter customerRepositoryAfterAfter, IThirdPartyPaymentServiceAfter paymentServiceAfter)
        {
            this.logger = logger;
            this.customerRepositoryAfterAfter = customerRepositoryAfterAfter;
            this.paymentServiceAfter = paymentServiceAfter;
        }

        public string RefillBalance(int customerId, decimal moneyAmount)
        {
            var moneyToCharge = MoneyAmount.Create(moneyAmount);
            var customer = customerRepositoryAfterAfter.GetById(customerId).ToResult("Customer can't be found");
            return Result.Combine(moneyToCharge, customer)
                .OnSuccess(() => customer.Value!.AddBalance(moneyToCharge.Value))
                .OnSuccess(() => paymentServiceAfter.ChargePayment(customer.Value!.BillingInfo, moneyToCharge.Value))
                .OnSuccess(
                    () => customerRepositoryAfterAfter.Save(customer.Value!)
                        .OnFailure(()=> paymentServiceAfter.RollbackLastTransaction()))
                .OnBoth(result => Log(result))
                .OnBoth(result => result.IsSuccess ? "OK" : result.Error);

        }

        private void Log(Result result)
        {
            logger.Log(result.IsFailure ? result.Error : "OK");
        }
    }
}
