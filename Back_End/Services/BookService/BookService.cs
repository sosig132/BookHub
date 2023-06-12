using Back_End.Data;
using Back_End.Models.Books;
using Back_End.Models.Categories;
using Back_End.Models.DTOs;
using Back_End.Models.Many_To_Many;
using Back_End.Repositories.BookRepository;
using Microsoft.EntityFrameworkCore;
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
        public Book GetBookMappedByTitle(string title)
        {
            Book book = _bookRepository.GetByTitle(title);
            //BookDTO bookDTO = new BookDTO()
            //{
            //    Title = book.Title,
            //    Image = book.Image
            //};
            return book;
        }
        public bool SaveChanges()
        {
            return _bookRepository.Save();
        }

        public Book AddBook(Book book)
        {
            _bookRepository.Create(book);
            return book;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _bookRepository.GetById(id);
        }
        public void AssociateCategories(List<Category> categories, Guid bookId)
        {
            foreach(var category in categories)
            {
                BookCategory bookCategory = new BookCategory
                {
                    BookId = bookId,
                    CategoryId = category.Id
                };
                _bookRepository.AddBookCategory(bookCategory);
            }
            _bookRepository.Save();
        }

        public void DeleteBook(Book book)
        {
            _bookRepository.Delete(book);
        }
    }
}
