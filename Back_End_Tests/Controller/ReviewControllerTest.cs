using Back_End.Controllers;
using Back_End.Models.Reviews;
using Back_End.Models.Users;
using Back_End.Services.BookService;
using Back_End.Services.ReviewService;
using Back_End.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End_Tests.Controller
{
    [TestFixture]
    public class ReviewControllerTests
    {
        private ReviewController _reviewController;
        private Mock<IReviewService> _reviewServiceMock;
        private Mock<IBookService> _bookServiceMock;
        private Mock<IUserService> _userServiceMock;

        [SetUp]
        public void Setup()
        {
            _reviewServiceMock = new Mock<IReviewService>();
            _bookServiceMock = new Mock<IBookService>();
            _userServiceMock = new Mock<IUserService>();

            _reviewController = new ReviewController(_reviewServiceMock.Object, _bookServiceMock.Object, _userServiceMock.Object);
        }

        [Test]
        public void AddReview_ValidReview_ReturnsOkResult()
        {
            // Arrange
            var review = new Review
            {
                Rating = 5,
                Content = "Great book",
                UserId = Guid.NewGuid(),
                BookId = Guid.NewGuid()
            };

            // Act
            var result = _reviewController.AddReview(review);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(review, okResult.Value);
            _reviewServiceMock.Verify(s => s.AddReview(review), Times.Once);
            _reviewServiceMock.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public void AddReview_InvalidReview_ReturnsBadRequest()
        {
            // Arrange
            var review = new Review(); // Review object without required properties

            // Act
            var result = _reviewController.AddReview(review);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Rating is null", badRequestResult.Value);
            _reviewServiceMock.Verify(s => s.AddReview(It.IsAny<Review>()), Times.Never);
            _reviewServiceMock.Verify(s => s.SaveChanges(), Times.Never);
        }

        [Test]
        public void GetReviewsByBookId_ExistingBookId_ReturnsOkResultWithReviews()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var userId= Guid.NewGuid();
            var reviews = new List<Review>
        {
            new Review { Id = Guid.NewGuid(), Rating = 4, Content = "Good book", UserId = userId, BookId = bookId },
            new Review { Id = Guid.NewGuid(), Rating = 5, Content = "Excellent book", UserId = userId, BookId = bookId }
        };
            _reviewServiceMock.Setup(s => s.GetReviewsByBookId(bookId)).Returns(reviews);
            _userServiceMock.Setup(s => s.GetUserMappedById(It.IsAny<Guid>())).Returns(new User { Id = userId, Email ="ads@fsan.com", DisplayName="sosig"});

            // Act
            var result = _reviewController.GetReviewsByBookId(bookId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(reviews, okResult.Value);
            _reviewServiceMock.Verify(s => s.GetReviewsByBookId(bookId), Times.Once);
            _userServiceMock.Verify(s => s.GetUserMappedById(It.IsAny<Guid>()), Times.Exactly(reviews.Count));
        }

        [Test]
        public void GetReviewsByBookId_NonExistingBookId_ReturnsNotFound()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _reviewServiceMock.Setup(s => s.GetReviewsByBookId(bookId)).Returns((List<Review>)null);

            // Act
            var result = _reviewController.GetReviewsByBookId(bookId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("Reviews not found", notFoundResult.Value);
            _reviewServiceMock.Verify(s => s.GetReviewsByBookId(bookId), Times.Once);
            _userServiceMock.Verify(s => s.GetUserMappedById(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public void UpdateReview_ExistingReviewId_ReturnsOkResultWithUpdatedReview()
        {
            var reviewId = Guid.NewGuid();
            var reviewToUpdate = new Review { Id = reviewId, Rating = 3, Content = "Average book" };
            var updatedReview = new Review { Id = reviewId, Rating = 4, Content = "Good book" };
            _reviewServiceMock.Setup(s => s.GetReviewById(reviewId)).Returns(reviewToUpdate);

            // Act
            var result = _reviewController.UpdateReview(reviewId, updatedReview);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;

            Assert.AreEqual(updatedReview.Content, reviewToUpdate.Content);
            Assert.AreEqual(updatedReview.Rating, reviewToUpdate.Rating);
            Assert.IsNotNull(reviewToUpdate.DateModified);

            _reviewServiceMock.Verify(s => s.UpdateReview(reviewToUpdate), Times.Once);
        }

        [Test]
        public void UpdateReview_NonExistingReviewId_ReturnsNotFound()
        {
            // Arrange
            var reviewId = Guid.NewGuid();
            _reviewServiceMock.Setup(s => s.GetReviewById(reviewId)).Returns((Review)null);

            // Act
            var result = _reviewController.UpdateReview(reviewId, new Review());

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("Review not found", notFoundResult.Value);
            _reviewServiceMock.Verify(s => s.UpdateReview(It.IsAny<Review>()), Times.Never);
            _reviewServiceMock.Verify(s => s.SaveChanges(), Times.Never);
        }

        [Test]
        public void DeleteReview_ExistingReviewId_ReturnsOkResultWithDeletedReview()
        {
            // Arrange
            var reviewId = Guid.NewGuid();
            var reviewToDelete = new Review { Id = reviewId };
            _reviewServiceMock.Setup(s => s.GetReviewById(reviewId)).Returns(reviewToDelete);

            // Act
            var result = _reviewController.DeleteReview(reviewId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(reviewToDelete, okResult.Value);
            _reviewServiceMock.Verify(s => s.DeleteReview(reviewToDelete), Times.Once);
            _reviewServiceMock.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteReview_NonExistingReviewId_ReturnsNotFound()
        {
            // Arrange
            var reviewId = Guid.NewGuid();
            _reviewServiceMock.Setup(s => s.GetReviewById(reviewId)).Returns((Review)null);

            // Act
            var result = _reviewController.DeleteReview(reviewId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("Review not found", notFoundResult.Value);
            _reviewServiceMock.Verify(s => s.DeleteReview(It.IsAny<Review>()), Times.Never);
            _reviewServiceMock.Verify(s => s.SaveChanges(), Times.Never);
        }
    }

}
