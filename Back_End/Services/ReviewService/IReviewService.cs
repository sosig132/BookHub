using Back_End.Models.Categories;
using Back_End.Models.Reviews;

namespace Back_End.Services.ReviewService
{
    public interface IReviewService
    {
        Review GetReviewById(Guid id);
        List<Review> GetReviewsByBookId(Guid id);
        List<Review> GetReviewsByUserId(Guid id);
        void AddReview(Review review);
        bool SaveChanges();
    }
}
