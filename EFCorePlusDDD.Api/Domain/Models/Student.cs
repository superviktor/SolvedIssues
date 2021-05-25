namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Student : Entity
    {
        protected Student()
        {
        }

        public Student(string name, string email, Course favoriteCourse):this()
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
        }

        public string Name { get; }
        public string Email { get; }
        public virtual Course FavoriteCourse { get; }
    }
}
