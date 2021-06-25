using GraphQL.Api.GraphQL.Type;
using GraphQL.Api.Repositories;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL
{
    public class ProductQuery: ObjectGraphType
    {
        public ProductQuery(IProductRepo repo)
        {
            Field<ListGraphType<ProductGt>>(
                "products",
                resolve: context => repo.GetAll());
        }
    }
}