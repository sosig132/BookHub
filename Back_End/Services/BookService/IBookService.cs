using Back_End.Models.Books;
using Back_End.Models.Categories;
using Back_End.Models.DTOs;
using Back_End.Models.Many_To_Many;

namespace Back_End.Services.BookService
{
    public interface IBookService
    {
        Book GetBookMappedByTitle(string title);
        Book AddBook(Book book);

        bool SaveChanges();

        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(Guid id);

        void AssociateCategories(List<Category> categories, Guid bookId);

        void DeleteBook(Book book);
    }
}
