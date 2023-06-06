using Back_End.Models.BookDetails;
using Back_End.Repositories.BookDetailsRepository;

namespace Back_End.Services.BookDetailsService
{
    public class BookDetailsService : IBookDetailsService
    {
        private readonly IBookDetailsRepository _bookDetailsRepository;
        public BookDetailsService(IBookDetailsRepository bookDetailsRepository)
        {
            _bookDetailsRepository = bookDetailsRepository;
        }
        public BookDetails GetBookDetailsById(Guid id)
        {
            return _bookDetailsRepository.GetBookDetailsById(id);
        }
    }
}
