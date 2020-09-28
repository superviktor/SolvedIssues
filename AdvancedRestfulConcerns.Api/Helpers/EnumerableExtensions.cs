using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace AdvancedRestfulConcerns.Api.Helpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var expandoObjects = new List<ExpandoObject>();
            var propertyInfos = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                propertyInfos.AddRange(typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance));
            }
            else
            {
                var fieldsAfterSplit = fields.Split(',');
                foreach (var field in fieldsAfterSplit)
                {
                    var propName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo == null)
                        throw new Exception($"Property {propName} can't be found for {typeof(TSource)}");

                    propertyInfos.Add(propertyInfo);
                }
            }

            foreach (var sourceObject in source)
            {
                var dataShapedObject = new ExpandoObject();
                foreach (var propertyInfo in propertyInfos)
                {
                    dataShapedObject.TryAdd(propertyInfo.Name, propertyInfo.GetValue(sourceObject));
                }

                expandoObjects.Add(dataShapedObject);
            }

            return expandoObjects;
        }
    }
}
