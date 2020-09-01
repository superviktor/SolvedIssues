using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain
{
    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> operand;

        public NotSpecification(Specification<T> operand)
        {
            this.operand = operand;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = operand.ToExpression();

            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}