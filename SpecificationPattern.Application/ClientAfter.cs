using System.Collections.Generic;
using System.Linq;
using SpecificationPattern.Domain;
using SpecificationPattern.Domain.Model;

namespace SpecificationPattern.Application
{
    public class ClientAfter
    {
        private readonly IMovieRepositoryAfter repository;

        public ClientAfter(IMovieRepositoryAfter repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<MovieDto> GetAllMovies(bool onlyForChildren, decimal minimumRating, bool availableOnCd, string? directedBy)
        {
            var spec = Specification<MovieAfter>.NoFilter;
            if (onlyForChildren)
                spec = spec.And(new MovieForChildrenSpecification());

            if (availableOnCd)
                spec = spec.And(new MovieAvailableOnCdSpecification());

            if (!string.IsNullOrEmpty(directedBy))
                spec = spec.And(new MovieDirectedBySpecification(directedBy));

            return (IReadOnlyList<MovieDto>)repository.GetAllMovies(spec).Select(m => new MovieDto());
        }

        public string BuyAdultTicket(int movieId)
        {
            var movie = repository.GetById(movieId);

            return movie == null ? "Movie doesn't exist" : "Adult ticket were bought successfully";
        }
        public string BuyChildTicket(int movieId)
        {
            var movie = repository.GetById(movieId);

            if (movie == null)
                return "Movie doesn't exist";

            var specification = new MovieForChildrenSpecification();
            if (!specification.IsSatisfiedBy(movie))
                return "This movie is not available for children";

            return "Children ticket were bought successfully";
        }
        public string BuyAvailableOnCdTicket(int movieId)
        {
            var movie = repository.GetById(movieId);

            if (movie == null)
                return "Movie doesn't exist";

            var specification = new MovieAvailableOnCdSpecification();
            if (!specification.IsSatisfiedBy(movie))
                return "This movie doesn't have CD version";

            return "Children ticket were bought successfully";
        }
    }
}
