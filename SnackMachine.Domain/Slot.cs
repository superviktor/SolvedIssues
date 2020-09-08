namespace SnackMachine.Domain
{
    public class Slot : Entity
    {
        public Slot(int position, SnackMachine snackMachine) : this()
        {
            Position = position;
            SnackMachine = snackMachine;
        }

        protected Slot()
        {
        }

        public int Position { get; }
        public SnackMachine SnackMachine { get; }
        public SnackPile SnackPile { get; set; }
    }
}