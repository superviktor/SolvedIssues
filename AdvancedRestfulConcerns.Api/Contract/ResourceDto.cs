using System;

namespace AdvancedRestfulConcerns.Api.Contract
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
