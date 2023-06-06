using Back_End.Data;
using Back_End.Models.BookDetails;
using Back_End.Models.Books;
using Back_End.Models.Many_To_Many;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Repositories.BookRepository
{
    public class BookRepository : GenericRepository.GenericRepository<Book>, IBookRepository
    {
        private readonly Context _context;
        public BookRepository(Context context) : base(context)
        {
            _context = context;
        }
        public List<Book> GetAllWithInclude()
        {
            return _context.Books.Include(x => x.BookDetails).ToList();
        }

        public Book GetByIdWithJoin(Guid id)
        {
            var result = _table.Join(_context.BookDetails, b => b.Id, bd => bd.BookId, (b, bd) => new { b, bd })
                .Where(x => x.b.Id == id)
                .Select(obj => obj.b);
            

            return (Book)result;
        }

        Book IBookRepository.GetByTitle(string title)
        {
            return _table.FirstOrDefault(x => x.Title.ToLower() == title.ToLower());
        }

        public BookCategory AddBookCategory(BookCategory bookCategory)
        {
            _context.BookCategories.Add(bookCategory);
            return bookCategory;
        }
    }
}
