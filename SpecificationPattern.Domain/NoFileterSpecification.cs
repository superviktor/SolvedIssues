using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain
{
    internal sealed class NoFilterSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return m => true;
        }
    }
}