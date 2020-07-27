namespace CqrsTemplate.Domain
{
    public class Model
    {
        private Model(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public static Model Create(string name)
        {
            return new Model(name);
        }
        public void Update(string name)
        {
            //validation here
            Name = name;
        }
    }
}
