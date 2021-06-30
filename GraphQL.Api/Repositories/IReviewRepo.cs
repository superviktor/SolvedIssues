using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Api.Entities;

namespace GraphQL.Api.Repositories
{
    public interface IReviewRepo
    {
        IEnumerable<Review> GetForProduct(int id);
        Task<ILookup<int, Review>> GetForProductsAsync(IEnumerable<int> ids);
        Task<Review> AddAsync(Review review);
    }
}