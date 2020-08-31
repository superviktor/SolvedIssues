using System.Collections.Generic;
using System.Linq;
using SpecificationPattern.Domain;
using SpecificationPattern.Domain.Model;

namespace SpecificationPattern.Application
{
    public class ClientMiddle
    {
        private readonly IMovieRepositoryMiddle repository;

        public ClientMiddle(IMovieRepositoryMiddle repository)
        {
            this.repository = repository;
        }

        public IReadOnlyList<MovieDto> GetAllMovies(bool onlyForChildren, decimal minimumRating, bool availableOnCd)
        {
            // how to combine expressions?
            var specification = new GenericSpecification<MovieMiddle>(MovieMiddle.IsSuitableForChildren);

            return (IReadOnlyList<MovieDto>)repository.GetAllMovies(specification).Select(m => new MovieDto());
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

            var specification = new GenericSpecification<MovieMiddle>(MovieMiddle.IsSuitableForChildren);
            if (!specification.IsSatisfiedBy(movie))
                return "This movie is not available for children";

            return "Children ticket were bought successfully";
        }
        public string BuyAvailableOnCdTicket(int movieId)
        {
            var movie = repository.GetById(movieId);

            if (movie == null)
                return "Movie doesn't exist";

            var specification = new GenericSpecification<MovieMiddle>(MovieMiddle.HasCdVersion);
            if (!specification.IsSatisfiedBy(movie))
                return "This movie doesn't have CD version";

            return "Children ticket were bought successfully";
        }
    }
}
