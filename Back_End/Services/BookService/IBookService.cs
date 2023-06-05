using Back_End.Models.Books;
using Back_End.Models.DTOs;

namespace Back_End.Services.BookService
{
    public interface IBookService
    {
        Book GetBookMappedByTitle(string title);
        Book AddBook(Book book);

        bool SaveChanges();

        Task<List<Book>> GetAllBooks();
    }
}
