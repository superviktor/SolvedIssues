namespace EFCorePlusDDD.Api.Dtos
{
    public class EditPersonalInfo
    {
        public long StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long NameSuffixId { get; set; }
        public string Email { get; set; }
        public long FavoriteCourseId { get; set; }
    }
}