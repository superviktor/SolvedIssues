namespace RailwayOrientedProgramming.Application
{
    public class CustomerServiceBefore
    {
        private readonly ILogger logger;
        private readonly ICustomerRepositoryBefore _repositoryBefore;
        private readonly IThirdPartyPaymentServiceBefore _paymentServiceBefore;
        public CustomerServiceBefore(ILogger logger, ICustomerRepositoryBefore repositoryBefore, IThirdPartyPaymentServiceBefore paymentServiceBefore)
        {
            this.logger = logger;
            this._repositoryBefore = repositoryBefore;
            this._paymentServiceBefore = paymentServiceBefore;
        }

        public string RefillBalance(int customerId, decimal moneyAmount)
        {
            if (!MoneyAmountIsValid(moneyAmount))
            {
                logger.Log("Money amount is invalid");

                return "Money amount is invalid";
            }

            var customer = _repositoryBefore.GetById(customerId);
            if (customer == null)
            {
                logger.Log("CustomerBefore is not found");

                return "CustomerBefore is not found";
            }

            customer.Balance += moneyAmount;

            try
            {
                _paymentServiceBefore.ChargePayment(customer.BillingInfo, moneyAmount);
            }
            catch (ChargeFailedException)
            {
                logger.Log("Unable to charge credit card");

                return "Unable to charge credit card";
            }

            try
            {
                _repositoryBefore.Save(customer);
            }
            catch (SqlException)
            {
                _paymentServiceBefore.RollbackLastTransaction();
                logger.Log("Unable to connect to the database");

                return "Unable to connect to the database";
            }

            logger.Log("OK");

            return "OK";
        }

        private bool MoneyAmountIsValid(in decimal moneyAmount)
        {
            return moneyAmount > 0;
        }
    }
}
