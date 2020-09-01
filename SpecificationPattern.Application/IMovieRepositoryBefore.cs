using SpecificationPattern.Domain.Model;
using System.Collections.Generic;

namespace SpecificationPattern.Application
{
    public interface IMovieRepositoryBefore
    {
        //return DbContext.Movies.Where(m=>m.MpaaRating <= MpaaRating.PG && m.ReleaseDate <= DateTime.Now.AddMonths(-6))
        //=> domain logic duplication
        IEnumerable<MovieBefore> GetAllMovies(bool onlyForChildren, decimal minimumRating, bool availableOnCd);
        MovieBefore? GetById(int id);
    }
}
