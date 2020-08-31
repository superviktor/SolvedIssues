using SpecificationPattern.Domain;
using SpecificationPattern.Domain.Model;
using System.Collections.Generic;

namespace SpecificationPattern.Application
{
    public interface IMovieRepositoryMiddle
    {
        //return DbContext.Movies.Where(expression)
        IReadOnlyList<MovieMiddle> GetAllMovies(GenericSpecification<MovieMiddle> genericSpecification);
        MovieMiddle? GetById(int id);
    }
}
