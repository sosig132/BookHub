using Back_End.Models.BookDetails;
using Back_End.Models.Books;
using Back_End.Models.CompositeBookDet;
using Back_End.Models.Many_To_Many;
using Back_End.Models.Reviews;
using Back_End.Services.BookDetailsService;
using Back_End.Services.BookService;
using Back_End.Services.CategoryService;
using Back_End.Services.ReviewService;
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
        private readonly IBookDetailsService _bookDetailsService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;
        public BookController(IBookService bookservice, IBookDetailsService bookDetailsService, ICategoryService categoryService, IReviewService reviewService)
        {
            _bookService = bookservice;
            _bookDetailsService = bookDetailsService;
            _categoryService = categoryService;
            _reviewService = reviewService;
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
                },
                BookCategories = new List<BookCategory>(),
                Reviews = new List<Review>()
            };
            
            book.BookCategories = bookd.Categories.Select(c => new BookCategory
            {
                BookId = book.Id,
                CategoryId = c.Id
            }).ToList();
            

            if (_bookService.GetBookMappedByTitle(book.Title) != null)
            {
                return BadRequest("Book already exists");
            }
            if (book.Title == null || book.Author == null || book.ISBN == null)
            {
                return BadRequest("Please fill all the fields");
            }

            if(book.BookCategories.Count == 0)
            {
                return BadRequest("Please select at least one category");
            }   

            _bookService.AddBook(book);
            _bookService.SaveChanges();

            return Ok(book);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();

            foreach(var book in books)
            {
                book.BookDetails = _bookDetailsService.GetBookDetailsById(book.Id);
                var categories = _categoryService.GetByBookId(book.Id);
                book.BookCategories = categories.Select(c => new BookCategory
                {
                    BookId = book.Id,
                    CategoryId = c.Id
                }).ToList();
                book.Reviews = _reviewService.GetReviewsByBookId(book.Id);
                foreach(var category in book.BookCategories)
                {
                    category.Category = _categoryService.GetById(category.CategoryId);
                }
            }
            return Ok(books);
        }
        [HttpGet("byId")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if(book == null)
            {
                return NotFound();
            }
            var bookDetails = _bookDetailsService.GetBookDetailsById(id);
            if(bookDetails == null)
            {
                return NotFound();
            }
            book.BookDetails = bookDetails;
            if(book.BookDetails == null)
            {
                return NotFound();
            }
            var categories = _categoryService.GetByBookId(id);

            book.BookCategories = categories.Select(c => new BookCategory
            {
                BookId = book.Id,
                CategoryId = c.Id
            }).ToList();
            book.Reviews = _reviewService.GetReviewsByBookId(book.Id);
            foreach (var category in book.BookCategories)
            {
                category.Category = _categoryService.GetById(category.CategoryId);
            }

            return Ok(book);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.DeleteBook(book);
            _bookService.SaveChanges();
            return Ok();
        }
        
    }
}
