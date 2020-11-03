using System;
using AsyncApi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsyncApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynchronousBooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public SynchronousBooksController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooks();

            return Ok(books);
        }
    }
}
