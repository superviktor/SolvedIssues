namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Course : Entity
    {
        public static readonly Course Calculus = new Course(1, "Calculus");
        public static readonly Course Chemistry = new Course(2, "Chemistry");

        protected Course()
        {
        }

        private Course(long id, string name) : base(id)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
