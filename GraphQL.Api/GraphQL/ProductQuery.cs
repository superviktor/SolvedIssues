using GraphQL.Api.GraphQL.Types;
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

            Field<ProductGt>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"}),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return repo.GetById(id);
                });
        }
    }
}