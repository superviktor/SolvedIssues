namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Student : Entity
    {
        protected Student()
        {
        }

        public Student(string name, string email, Course favouriteCourse):this()
        {
            Name = name;
            Email = email;
            FavouriteCourse = favouriteCourse;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public virtual Course FavouriteCourse { get; private set; }
    }
}
