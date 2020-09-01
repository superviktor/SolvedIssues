using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain
{
    public abstract class Specification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();

            return predicate(entity);
        }

        public static readonly Specification<T> NoFilter = new NoFilterSpecification<T>();

        public Specification<T> And(Specification<T> spec)
        {
            if (this == NoFilter)
                return spec;
            if (spec == NoFilter)
                return this;

            return new AndSpecification<T>(this, spec);
        }

        public Specification<T> Or(Specification<T> spec)
        {
            if (this == NoFilter || spec == NoFilter)
                return NoFilter;

            return new OrSpecification<T>(this, spec);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}