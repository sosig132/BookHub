using Back_End.Models.BookDetails;

namespace Back_End.Services.BookDetailsService
{
    public interface IBookDetailsService
    {
        BookDetails GetBookDetailsById(Guid id);
    }
}
