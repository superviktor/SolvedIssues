using System;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL.ConsoleClient.Clients;

namespace GraphQL.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //handwritten client
            var productHttpClient = new ProductHttpClient();
            var result = await productHttpClient.GetProducts();
            Console.WriteLine(JsonSerializer.Serialize(result));

            //library
            var productGraphQlClient = new ProductGraphQlClient();
            var prod1 = await productGraphQlClient.GetProduct(1);
            Console.WriteLine(JsonSerializer.Serialize(prod1));
            Console.ReadKey();
        }
    }
}
