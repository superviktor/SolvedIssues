#nullable enable
using System;

namespace ModernCSharpTechniques.Domain.After
{
    public class PersonAfter
    {
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => firstName = value ?? throw new ArgumentNullException(nameof(value));
        }

        //look warning
        public string HyhpenateForPartner(PersonAfter partner)
        {
            _ = partner ?? throw new ArgumentNullException(nameof(partner));

            return $"{this.FirstName} - {partner.FirstName}";
        }
    }
}
