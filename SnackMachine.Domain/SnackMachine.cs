namespace SnackMachine.Domain
{
    public sealed class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; }
        public Money MoneyInTransaction { get; private set; }

        public void InsertMoney(Money moneyToInsert)
        {
            MoneyInTransaction += moneyToInsert;
        }

        public void ReturnMoney()
        {
            //MoneyInTransaction = 0;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            //MoneyInTransaction = 0;
        }
    }
}