namespace SnackMachine.Domain
{
    public class Snack : Entity
    {
        public Snack(string name) : base()
        {
            Name = name;
        }

        public string Name { get; }
    }
}
