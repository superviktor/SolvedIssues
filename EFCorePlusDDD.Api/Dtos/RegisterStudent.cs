namespace EFCorePlusDDD.Api.Dtos
{
    public class RegisterStudent
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long FavoriteCourseId { get; set; }
        public Grade FavoriteCourseGrade { get; set; }
    }
}