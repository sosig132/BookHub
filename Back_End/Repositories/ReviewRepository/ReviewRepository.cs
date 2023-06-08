using Back_End.Data;
using Back_End.Models.Reviews;

namespace Back_End.Repositories.ReviewRepository
{
    public class ReviewRepository : GenericRepository.GenericRepository<Review>, IReviewRepository
    {
        private readonly Context _context;
        public ReviewRepository(Context context) : base(context)
        {
            _context = context;
        }

        public Review GetReviewById(Guid id)
        {
            return _table.FirstOrDefault(x => x.Id == id);
        }

        public List<Review> GetReviewsByBookId(Guid id)
        {
            return _table.Where(x => x.BookId == id).ToList();
        }
        public List<Review> GetReviewsByUserId(Guid id)
        {
            return _table.Where(x => x.UserId == id).ToList();
        }

        
    }
}
