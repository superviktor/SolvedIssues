using System;
using System.Linq.Expressions;
using SpecificationPattern.Domain.Model;

namespace SpecificationPattern.Domain
{
    public class MovieForChildrenSpecification : Specification<MovieAfter>
    {
        public override Expression<Func<MovieAfter, bool>> ToExpression()
        {
            return m => m.MpaaRating <= MpaaRating.PG;
        }
    }
}
