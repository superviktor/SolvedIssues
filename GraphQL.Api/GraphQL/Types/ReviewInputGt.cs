using GraphQL.Types;

namespace GraphQL.Api.GraphQL.Types
{
    public class ReviewInputGt : InputObjectGraphType
    {
        public ReviewInputGt()
        {
            Name = "reviewInput";
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("content");
            Field<NonNullGraphType<IntGraphType>>("productId");
        }   
    }
}