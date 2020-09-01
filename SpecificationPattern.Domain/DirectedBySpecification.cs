using System;
using System.Linq.Expressions;
using SpecificationPattern.Domain.Model;

namespace SpecificationPattern.Domain
{
    public class MovieDirectedBySpecification : Specification<MovieAfter>
    {
        private readonly string directedBy;

        public MovieDirectedBySpecification(string directedBy)
        {
            this.directedBy = directedBy;
        }

        public override Expression<Func<MovieAfter, bool>> ToExpression()
        {
            return m => m.DirectedBy == directedBy;
        }
    }
}
