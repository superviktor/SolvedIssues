namespace SnackMachine.Domain
{
    public class Slot : Entity
    {
        public Slot(Snack snack, int quantity, decimal price, int position, SnackMachine snackMachine)
        {
            Snack = snack;
            Quantity = quantity;
            Price = price;
            Position = position;
            SnackMachine = snackMachine;
        }

        public Snack Snack { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Position { get; }
        public SnackMachine SnackMachine { get; }
    }
}