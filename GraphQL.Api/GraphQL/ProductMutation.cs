using GraphQL.Api.Entities;
using GraphQL.Api.GraphQL.Messaging;
using GraphQL.Api.GraphQL.Types;
using GraphQL.Api.Repositories;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL
{
    public class ProductMutation : ObjectGraphType
    {
        public ProductMutation(IReviewRepo reviewRepo, ReviewMessageService messageService)
        {
            FieldAsync<ReviewGt>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ReviewInputGt>> { Name = "review" }),
                resolve: async context =>
                {
                    var review = context.GetArgument<Review>("review");
                    await reviewRepo.AddAsync(review);
                    messageService.AddReviewAddedMessage(review);
                    return review;
                });
        }
    }
}