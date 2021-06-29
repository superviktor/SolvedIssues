using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Api.Entities;

namespace GraphQL.Api.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly List<Review> _reviews = new()
        {
            new Review
            {
                Id = 1,
                Title = "Title 1",
                Content = "Content 1",
                ProductId = 1
            },
            new Review
            {
                Id = 2,
                Title = "Title 2",
                Content = "Content 2",
                ProductId = 2
            }
        };

        public IEnumerable<Review> GetForProduct(int id)
        {
            return _reviews.Where(r => r.ProductId == id);
        }

        public Task<ILookup<int, Review>> GetForProductsAsync(IEnumerable<int> ids)
        {
            var reviews = _reviews.Where(pr => ids.Contains(pr.ProductId));
            return Task.FromResult(reviews.ToLookup(r => r.ProductId));
        }
    }
}
