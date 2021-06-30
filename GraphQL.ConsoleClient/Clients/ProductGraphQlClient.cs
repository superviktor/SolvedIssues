using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.ConsoleClient.Models;
using GraphQL.Client.Serializer.SystemTextJson;

namespace GraphQL.ConsoleClient.Clients
{
    public class ProductGraphQlClient
    {
        public async Task<ProductModel> GetProduct(int id)
        {
            var client = new GraphQLHttpClient("https://localhost:5001/graphql", new SystemTextJsonSerializer());
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
    }
}