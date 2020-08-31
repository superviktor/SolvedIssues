using System;

namespace SpecificationPattern.Domain.Model
{
    public class MovieAfter
    {
        protected MovieAfter()
        {
        }

        public int Id { get; }
        public string Name { get; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get;}
        public decimal Rating { get;}
        public MpaaRating MpaaRating { get; }
    }
}
