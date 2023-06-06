using Back_End.Data;
using Back_End.Models.BookDetails;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.BookDetailsRepository
{
    public class BookDetailsRepository : GenericRepository.GenericRepository<BookDetails>, IBookDetailsRepository
    {
        private readonly Context _context;
        public BookDetailsRepository(Context context) : base(context)
        {
            _context = context;
        }

        public BookDetails GetBookDetailsById(Guid id)
        {
            return _table.FirstOrDefault(x => x.BookId == id);
        }
    }
}
