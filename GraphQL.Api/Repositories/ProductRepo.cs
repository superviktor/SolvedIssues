using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Api.Entities;

namespace GraphQL.Api.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly IEnumerable<Product> _products = new[]
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
        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.SingleOrDefault(p => p.Id == id);
        }
    }
}