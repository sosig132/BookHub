using Back_End.Models.Reviews;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.ReviewRepository
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Review GetReviewById(Guid id);
        List<Review> GetReviewsByBookId(Guid id);

        List<Review> GetReviewsByUserId(Guid id);


    }
}
