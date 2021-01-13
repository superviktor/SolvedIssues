namespace EFCorePlusDDD.Api.Domain.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long FavouriteCourseId { get; set; }
    }
}
