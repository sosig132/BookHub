using Back_End.Models.BookDetails;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.BookDetailsRepository
{
    public interface IBookDetailsRepository : IGenericRepository<BookDetails>
    {
        BookDetails GetBookDetailsById(Guid id);
    }
}
