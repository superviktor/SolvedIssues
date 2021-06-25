using System;
using System.Collections.Generic;
using GraphQL.Api.Models;

namespace GraphQL.Api.Repositories
{
    public class ProductRepo : IProductRepo
    {
        public IEnumerable<Product> GetAll()
        {
            return new[]
            {
                new Product
                {
                    Id = 1,
                    Description = "ball size 5",
                    IntroducedAt = DateTimeOffset.UtcNow.AddDays(-3),
                    Name = "Abibas ball,",
                    Price = 10,
                    ProductType = ProductType.Sport,
                    Rating = 1,
                    Stock = 100
                },
                new Product
                {
                    Id = 2,
                    Description = "tomato ketchup and mayo",
                    IntroducedAt = DateTimeOffset.UtcNow.AddDays(-8),
                    Name = "Ketchoness,",
                    Price = 1,
                    ProductType = ProductType.Food,
                    Rating = 1,
                    Stock = 200
                }
            };
        }
    }
}