#nullable enable
using System;

namespace ModernCSharpTechniques.Domain.Before
{
    public class PersonBefore
    {
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                firstName = value;
            }
        }

        //look warning
        public string HyhpenateForPartner(PersonBefore partner)
        {
            if (partner != null)
            {
                return $"{this.FirstName} - {partner.FirstName}";
            }

            throw new ArgumentNullException(nameof(partner));
        }
    }
}
