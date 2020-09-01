using System;

namespace SpecificationPattern.Domain.Model
{
    public class MovieBefore
    {
        protected MovieBefore()
        {
        }

        public int Id { get; }
        public string Name { get; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get;}
        public decimal Rating { get;}
        public MpaaRating MpaaRating { get; }
    }

    public enum MpaaRating
    {
        G,
        PG,
        PG13,
        R
    }
}
