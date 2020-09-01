using SpecificationPattern.Domain;
using SpecificationPattern.Domain.Model;
using System.Collections.Generic;

namespace SpecificationPattern.Application
{
    public interface IMovieRepositoryAfter
    {
        //return DbContext.Movies.Where(specification.ToExpression())
        IReadOnlyList<MovieAfter> GetAllMovies(Specification<MovieAfter> specification);
        MovieAfter? GetById(int id);
    }
}
