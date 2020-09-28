using System;

namespace AdvancedRestfulConcerns.Api.Model
{
    public class Resource
    {
        public Resource(Guid id, string firstName, string lastName, DateTime dateOfBirth, DateTime? dateOfDeath)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get;}
        public DateTime DateOfBirth { get; }
        public DateTime? DateOfDeath { get; }
    }
}
