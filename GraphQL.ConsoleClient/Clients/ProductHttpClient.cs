using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL.ConsoleClient.Models;

namespace GraphQL.ConsoleClient.Clients
{
    public class ProductHttpClient
    {
        public async Task<Response<ProductsContainer>> GetProducts()
        {
            var httpClient = new HttpClient {BaseAddress = new Uri("https://localhost:5001/graphql")};
            var response = await httpClient.GetAsync(@"?query= 
            { products 
                { id name price } 
            }");
            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Response<ProductsContainer>>(stringResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}