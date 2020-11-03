using System;
using System.Threading.Tasks;
using AsyncApi.Api.Filters;
using AsyncApi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsyncApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [HttpGet]
        [BooksResultFilter]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetBooksAsync();

            return Ok(books);
        }


        [HttpGet]
        [Route("{id}")]
        [BookResultFilter]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _bookRepository.GetBookAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}
