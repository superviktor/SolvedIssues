namespace RailwayOrientedProgramming.Domain
{
    public class CustomerAfter
    {
        public decimal Balance { get; private set; }
        public BillingInfo BillingInfo { get; }

        public void AddBalance(MoneyAmount moneyAmount)
        {
            Balance += moneyAmount.Value;
        }
    }
}
