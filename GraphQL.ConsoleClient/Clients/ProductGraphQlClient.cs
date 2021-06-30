using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.ConsoleClient.Models;
using GraphQL.Client.Serializer.SystemTextJson;

namespace GraphQL.ConsoleClient.Clients
{
    public class ProductGraphQlClient
    {
        private readonly string httpsLocalhostGraphql = "https://localhost:5001/graphql";

        public async Task<ProductModel> GetProduct(int id)
        {
            var client = new GraphQLHttpClient(httpsLocalhostGraphql, new SystemTextJsonSerializer());
            var query = new GraphQLHttpRequest
            {
                Query = @" 
                query productQuery($productId: ID!)
                { product(id: $productId) 
                    { id name price 
                    }
                }",
                Variables = new { productId = id }
            };
            var response = await client.SendQueryAsync<ProductResponse>(query);
            return response.Data.Product;
        }

        public async Task<ProductReviewModel> AddReview(ProductReviewModelInput review)
        {
            var client = new GraphQLHttpClient(httpsLocalhostGraphql, new SystemTextJsonSerializer());
            var query = new GraphQLHttpRequest
            {
                Query = @" 
                mutation($review: reviewInput!)
                {
                    createReview(review: $review)
                    {
                        id title
                    }
                }",
                Variables = new { review }
            };
            var response = await client.SendMutationAsync<ProductReviewResponse>(query);
            return response.Data.CreateReview;
        }
    }
}