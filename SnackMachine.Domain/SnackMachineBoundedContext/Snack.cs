using SnackMachine.Domain.Base;

namespace SnackMachine.Domain.SnackMachineBoundedContext
{
    public class Snack : AggregateRoot
    {
        public static readonly Snack None = new Snack("None");
        public static readonly Snack Chocolate = new Snack("Chocolate");
        public static readonly Snack Soda = new Snack("Soda");
        public static readonly Snack Gum = new Snack("Gum");

        private Snack(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
