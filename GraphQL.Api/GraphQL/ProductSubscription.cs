using GraphQL.Api.GraphQL.Messaging;
using GraphQL.Api.GraphQL.Types;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL
{
    public class ProductSubscription: ObjectGraphType
    {
        public ProductSubscription(ReviewMessageService messageService)
        {
            Name = "Subscription";
            AddField(new EventStreamFieldType
            {
                Name = "reviewAdded",
                Type = typeof(ReviewAddedMessageGt),
                Resolver = new FuncFieldResolver<ReviewAddedMessage>(c => c.Source as ReviewAddedMessage),
                Subscriber = new EventStreamResolver<ReviewAddedMessage>(c => messageService.GetMessages())
            });
        }
    }
}
