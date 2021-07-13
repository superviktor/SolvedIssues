namespace CSharpObserveVersions.Deconstruction
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void Deconstruct(out string name, out int age)
        {
            name = Name;
            age = Age;
        }
    }
}
