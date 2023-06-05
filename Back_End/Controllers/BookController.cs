using Back_End.Models.BookDetails;
using Back_End.Models.Books;
using Back_End.Models.CompositeBookDet;
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
        public IActionResult AddBook([FromBody] CompositeBookDet bookd)
        {
            bookd.Id= Guid.NewGuid();
            Book book = new()
            {
                DateCreated = DateTime.Now,
                Id = bookd.Id,
                Title = bookd.Title,
                Author = bookd.Author,
                ISBN = bookd.ISBN,
                Image = bookd.Image,
                BookDetails = new BookDetails
                {
                    DateCreated = DateTime.Now,
                    Description = bookd.Description,
                    PageCount = bookd.PageCount,
                    Publisher = bookd.Publisher,
                    Language = bookd.Language,
                    BookId = bookd.Id
                }
            };

            if (_bookService.GetBookMappedByTitle(book.Title) != null)
            {
                return BadRequest("Book already exists");
            }
            if (book.Title == null || book.Author == null || book.ISBN == null)
            {
                return BadRequest("Please fill all the fields");
            }

            _bookService.AddBook(book);
            _bookService.SaveChanges();

            return Ok(book);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }
    }
}
