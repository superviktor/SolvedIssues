namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Student
    {
        public Student(string name, string email, long favouriteCourseId)
        {
            Name = name;
            Email = email;
            FavouriteCourseId = favouriteCourseId;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public long FavouriteCourseId { get; private set; }
    }
}
