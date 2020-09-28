using System;
using System.Collections.Generic;

namespace AdvancedRestfulConcerns.Api.Helpers
{
    public class PropertyMappingValue
    {
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            DestinationProperties = destinationProperties ?? throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }

        public IEnumerable<string> DestinationProperties { get; }
        public bool Revert { get;  }
    }
}
