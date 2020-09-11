using SnackMachine.Domain.Base;

namespace SnackMachine.Domain.SnackMachineBoundedContext
{
    public class Slot : Entity
    {
        public Slot(int position, SnackMachine snackMachine) : this()
        {
            Position = position;
            SnackMachine = snackMachine;
            SnackPile = SnackPile.Empty;
        }

        protected Slot()
        {
        }

        public int Position { get; }
        public SnackMachine SnackMachine { get; }
        public SnackPile SnackPile { get; set; }
    }
}