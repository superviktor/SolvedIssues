namespace EFCorePlusDDD.Api.Dtos
{
    public class EditPersonalInfo
    {
        public long StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long FavoriteCourseId { get; set; }
    }
}