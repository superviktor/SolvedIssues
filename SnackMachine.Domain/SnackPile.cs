using System;

namespace SnackMachine.Domain
{
    public sealed class SnackPile : ValueObject<SnackPile>
    {
        private SnackPile()
        {
        }

        public SnackPile(Snack snack, int quantity, decimal price) : this()
        {
            if(snack == null)
                throw new InvalidOperationException();
            if (quantity < 0)
                throw new InvalidOperationException();
            if(price <= 0)
                throw new InvalidOperationException();
            if(price % 0.01m > 0)
                throw new InvalidOperationException();

            Snack = snack;
            Quantity = quantity;
            Price = price;
        }

        public Snack Snack { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        protected override bool EqualsCore(SnackPile other)
        {
            return Snack == other.Snack &&
                   Quantity == other.Quantity &&
                   Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = Snack.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();

                return hashCode;
            }
        }

        public SnackPile SubtractOne()
        {
            return new SnackPile(Snack, Quantity -1, Price);
        }
    }
}