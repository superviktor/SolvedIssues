using System;

namespace GraphQL.Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset IntroducedAt { get; set; }
        public string PhotoFileName { get; set; }
    }

    public enum ProductType
    {
        Food,
        Sport,
        Entertainment
    }
}