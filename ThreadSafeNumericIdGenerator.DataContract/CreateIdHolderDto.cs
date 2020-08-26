using System;
using System.ComponentModel.DataAnnotations;

namespace ThreadSafeNumericIdGenerator.DataContract
{
    public class CreateIdHolderDto
    {
        [Required]
        public string Name { get; set; }
        public long? StartFrom { get; set; }

    }
}
