using System;
using System.Collections.Generic;
using System.Linq;
using SpecificationPattern.Domain.Model;

namespace SpecificationPattern.Application
{
    public class ClientBefore
    {
        private readonly IMovieRepositoryBefore repository;

        public ClientBefore(IMovieRepositoryBefore repository)
        {
            this.repository = repository;
        }

        public IEnumerable<MovieDto> GetAllMovies(bool onlyForChildren, decimal minimumRating, bool availableOnCd)
        {
            return repository.GetAllMovies(onlyForChildren, minimumRating, availableOnCd).Select(m=> new MovieDto());
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
            //dublicated in repository
            if (movie.MpaaRating > MpaaRating.PG)
                return "This movie is not available for children";

            return "Children ticket were bought successfully";
        }
        public string BuyAvailableOnCdTicket(int movieId)
        {
            var movie = repository.GetById(movieId);

            if (movie == null)
                return "Movie doesn't exist";
            //dublicated in repository
            if (movie.ReleaseDate <= DateTime.Now.AddMonths(-6))
                return "This movie doesn't have CD version";

            return "Children ticket were bought successfully";
        }
    }
}
