using System.Collections.Generic;
using GraphQL.Api.Entities;

namespace GraphQL.Api.Repositories
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
    }
}