using Back_End.Models.Books;
using Back_End.Models.DTOs;
using Back_End.Repositories.BookRepository;
using Microsoft.Identity.Client;

namespace Back_End.Services.BookService
{
    public class BookService : IBookService
    {
        public IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public BookDTO GetBookMappedByTitle(string title)
        {
            Book book = _bookRepository.GetByTitle(title);
            BookDTO bookDTO = new BookDTO()
            {
                Title = book.Title,
                Image = book.Image
            };
            return bookDTO;
        }
    }
}
