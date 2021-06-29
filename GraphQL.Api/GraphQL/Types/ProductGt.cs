using System.Security.Claims;
using GraphQL.Api.Entities;
using GraphQL.Api.Repositories;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL.Types
{
    public sealed class ProductGt : ObjectGraphType<Product>
    {
        public ProductGt(IReviewRepo reviewRepo, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
            Field(t => t.IntroducedAt);
            Field(t => t.PhotoFileName);
            Field(t => t.Price);
            Field(t => t.Rating);
            Field(t => t.Stock);
            Field<ProductTypeGt>("ProductType");
            Field<ListGraphType<ReviewGt>>()
                .Name("reviews")
                .Resolve(context =>
                {
                    //for auth
                    //var user = (ClaimsPrincipal)context.UserContext["User"];
                    var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<int, Review>("GetReviewsByProductId", reviewRepo.GetForProductsAsync);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
