namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Student
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

        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public virtual Course FavouriteCourse { get; private set; }
    }
}
