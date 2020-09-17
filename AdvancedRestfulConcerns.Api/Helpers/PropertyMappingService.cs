using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedRestfulConcerns.Api.Helpers
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly IList<PropertyMapping> propertyMappings = new List<PropertyMapping>();

        private readonly Dictionary<string, PropertyMappingValue> resourcePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Name", new PropertyMappingValue(new List<string> {"FirstName", "LastName"})},
                {"Age", new PropertyMappingValue(new List<string> {"DateOfBirth"}, true)}
            };

        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping(resourcePropertyMapping));
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
                return true;

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                    return false;
            }
            return true;
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchMapping = propertyMappings.OfType<PropertyMapping>();

            var enumerable = matchMapping.ToList();
            if (enumerable.Count() == 1)
                return enumerable.First().MappingDictionary;

            throw new Exception($"Can't find property mapping {typeof(TSource)}-{typeof(TDestination)}");
        }
    }
}