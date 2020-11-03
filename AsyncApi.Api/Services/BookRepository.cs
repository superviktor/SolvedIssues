using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsyncApi.Api.Entities;

namespace AsyncApi.Api.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] _books =
        {
            new Book {Id = Guid.NewGuid(), Title = "White and black"},
            new Book {Id = Guid.NewGuid(), Title = "Bible"}
        };

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            await Task.Delay(2000);
            return await Task.FromResult(_books);
        }

        public IEnumerable<Book> GetBooks()
        {
            Thread.Sleep(2000);
            return _books;
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await Task.FromResult(_books.FirstOrDefault(b => b.Id == id));
        }
    }
}