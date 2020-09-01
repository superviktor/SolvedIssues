using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Domain.Model
{
    public class MovieMiddle
    {
        public static Expression<Func<MovieMiddle, bool>> IsSuitableForChildren = m => m.MpaaRating <= MpaaRating.PG;
        public static Expression<Func<MovieMiddle, bool>> HasCdVersion = m => m.ReleaseDate <= DateTime.Now.AddMonths(-6);

        protected MovieMiddle()
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
