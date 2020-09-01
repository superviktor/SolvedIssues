using System;
using System.Linq.Expressions;
using SpecificationPattern.Domain.Model;

namespace SpecificationPattern.Domain
{
    public class MovieAvailableOnCdSpecification : Specification<MovieAfter>
    {
        public override Expression<Func<MovieAfter, bool>> ToExpression()
        {
            return m => m.ReleaseDate <= DateTime.Now.AddMonths(-6);
        }
    }
}