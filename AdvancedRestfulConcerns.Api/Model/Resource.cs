using System;

namespace AdvancedRestfulConcerns.Api.Model
{
    public class Resource
    {
        public Resource(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public string FirstName { get; }
        public string LastName { get;}
        public DateTime DateOfBirth { get; }
    }
}
