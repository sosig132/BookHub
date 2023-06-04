using Back_End.Models.Books;
using Back_End.Services.BookService;
using Back_End.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookservice)
        {
            _bookService = bookservice;

        }

        [HttpPost]
        [Route("addBook")]
        public IActionResult AddBook([FromBody] Book book)
        {
            book.DateCreated = DateTime.Now;
            book.Id=Guid.NewGuid();

            if(book == null)
            {
                return BadRequest("Book is null");
            }

            if(_bookService.GetBookMappedByTitle(book.Title) != null)
            {
                return BadRequest("Book already exists");
            }

            _bookService.AddBook(book);
            _bookService.SaveChanges();
            return Ok(book);
        }
    }
}
