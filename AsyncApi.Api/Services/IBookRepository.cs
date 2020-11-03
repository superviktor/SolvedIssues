using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncApi.Api.Entities;

namespace AsyncApi.Api.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        IEnumerable<Book> GetBooks();
        Task<Book> GetBookAsync(Guid id);
    }
}