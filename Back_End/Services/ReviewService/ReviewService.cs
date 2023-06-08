using Back_End.Models.Reviews;
using Back_End.Repositories.ReviewRepository;

namespace Back_End.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public Review GetReviewById(Guid id)
        {
            return _reviewRepository.GetReviewById(id);
        }
        public List<Review> GetReviewsByBookId(Guid id)
        {
            return _reviewRepository.GetReviewsByBookId(id);
        }
        public List<Review> GetReviewsByUserId(Guid id)
        {
            return _reviewRepository.GetReviewsByUserId(id);
        }

        public void AddReview(Review review)
        {
            _reviewRepository.Create(review);
            
        }

        public bool SaveChanges()
        {
            return _reviewRepository.Save();
        }
        
    }
}
