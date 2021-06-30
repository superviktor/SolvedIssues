using GraphQL.Api.Entities;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL.Types
{
    public sealed class ReviewGt : ObjectGraphType<Review>
    {
        public ReviewGt()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Content);
        }   
    }
}