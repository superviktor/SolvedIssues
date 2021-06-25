using GraphQL.Api.Models;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL.Type
{
    public class ProductGt : ObjectGraphType<Product>
    {
        public ProductGt()
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
        }
    }
}