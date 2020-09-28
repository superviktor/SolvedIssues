using System;
using System.Collections.Generic;

namespace AdvancedRestfulConcerns.Api.Helpers
{
    public class PropertyMapping
    {
        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary ?? throw new ArgumentNullException(nameof(mappingDictionary));
        }

        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; }
    }
}
