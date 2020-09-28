using System;

namespace AdvancedRestfulConcerns.Api.Contract
{
    public class ResourceCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
