using Back_End.Models.DTOs;

namespace Back_End.Services.BookService
{
    public interface IBookService
    {
        BookDTO GetBookMappedByTitle(string title);
    }
}
