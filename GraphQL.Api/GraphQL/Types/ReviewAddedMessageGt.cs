using GraphQL.Api.GraphQL.Messaging;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL.Types
{
    public class ReviewAddedMessageGt: ObjectGraphType<ReviewAddedMessage>
    {
        public ReviewAddedMessageGt()
        {
            Field(t => t.ProductId);
            Field(t => t.Title);
        }
    }
}
