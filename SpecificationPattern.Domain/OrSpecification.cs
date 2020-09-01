using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain
{
    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = left.ToExpression();
            var rightExpression = right.ToExpression();

            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }
}