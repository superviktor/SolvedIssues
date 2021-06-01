using System;

namespace CSharpObserveVersions.IntefaceDefaultMethods
{
    public interface IOrder
    {
        DateTime Purchased { get; }
        decimal Cost { get; }
    }
}