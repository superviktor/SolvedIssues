using System;

namespace AdvancedRestfulConcerns.Api.Contract
{
    public class ResourceFullDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
    }
}
