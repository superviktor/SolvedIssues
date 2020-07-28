using System;

namespace CqrsTemplate.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class DataStoreRetryAttribute : Attribute
    {
    }
}
