using System.Collections.Generic;
using GraphQL.Api.Models;

namespace GraphQL.Api.Repositories
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetAll();
    }
}