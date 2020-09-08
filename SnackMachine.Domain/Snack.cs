namespace SnackMachine.Domain
{
    public class Snack : AggregateRoot
    {
        public Snack(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
