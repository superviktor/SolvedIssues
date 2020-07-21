namespace CqrsTemplate.Domain
{
    public class Model
    {
        public string Name { get; private set; }
        public void Update(string name)
        {
            //validation here
            Name = name;
        }
    }
}
