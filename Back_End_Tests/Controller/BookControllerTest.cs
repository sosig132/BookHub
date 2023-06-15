using Back_End.Controllers;
using Back_End.Models.BookDetails;
using Back_End.Models.Books;
using Back_End.Models.Categories;
using Back_End.Models.CompositeBookDet;
using Back_End.Models.Reviews;
using Back_End.Services.BookDetailsService;
using Back_End.Services.BookService;
using Back_End.Services.CategoryService;
using Back_End.Services.ReviewService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End_Tests.Controller
{
    internal class BookControllerTest
    {
        [TestFixture]
        public class BookControllerTests
        {
            private BookController _bookController;
            private Mock<IBookService> _bookServiceMock;
            private Mock<IBookDetailsService> _bookDetailsServiceMock;
            private Mock<ICategoryService> _categoryServiceMock;
            private Mock<IReviewService> _reviewServiceMock;

            [SetUp]
            public void Setup()
            {
                _bookServiceMock = new Mock<IBookService>();
                _bookDetailsServiceMock = new Mock<IBookDetailsService>();
                _categoryServiceMock = new Mock<ICategoryService>();
                _reviewServiceMock = new Mock<IReviewService>();

                _bookController = new BookController(
                    _bookServiceMock.Object,
                    _bookDetailsServiceMock.Object,
                    _categoryServiceMock.Object,
                    _reviewServiceMock.Object
                );
            }

            [Test]
            public void AddBook_ValidBookData_ReturnsOkResultWithBook()
            {
                // Arrange
                var bookToAdd = new CompositeBookDet
                {
                    Title = "Book Title",
                    Author = "Author Name",
                    ISBN = "1234567890",
                    Image = "image-url",
                    Description = "Book Description",
                    PageCount = 200,
                    Publisher = "Publisher Name",
                    Language = "English",
                    Categories = new List<Category> { new Category { Id = Guid.NewGuid(), Name = "Category 1" } }
                };

                _bookServiceMock.Setup(s => s.GetBookMappedByTitle(bookToAdd.Title)).Returns(null as Book);
                _bookServiceMock.Setup(s => s.AddBook(It.IsAny<Book>())).Verifiable();
                _bookServiceMock.Setup(s => s.SaveChanges()).Verifiable();

                // Act
                var result = _bookController.AddBook(bookToAdd);

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okResult = (OkObjectResult)result;
                var returnedBook = (Book)okResult.Value;
                Assert.IsNotNull(returnedBook);
                Assert.AreEqual(bookToAdd.Title, returnedBook.Title);
                Assert.AreEqual(bookToAdd.Author, returnedBook.Author);
                Assert.AreEqual(bookToAdd.ISBN, returnedBook.ISBN);
                Assert.AreEqual(bookToAdd.Image, returnedBook.Image);
                Assert.IsNotNull(returnedBook.BookDetails);
                Assert.AreEqual(bookToAdd.Description, returnedBook.BookDetails.Description);
                Assert.AreEqual(bookToAdd.PageCount, returnedBook.BookDetails.PageCount);
                Assert.AreEqual(bookToAdd.Publisher, returnedBook.BookDetails.Publisher);
                Assert.AreEqual(bookToAdd.Language, returnedBook.BookDetails.Language);
                Assert.IsNotNull(returnedBook.BookCategories);
                Assert.AreEqual(bookToAdd.Categories.Count, returnedBook.BookCategories.Count);
                Assert.AreEqual(bookToAdd.Categories.Single().Id, returnedBook.BookCategories.Single().CategoryId);
                Assert.IsNotNull(returnedBook.Reviews);
                Assert.IsEmpty(returnedBook.Reviews);

                _bookServiceMock.Verify(s => s.GetBookMappedByTitle(bookToAdd.Title), Times.Once);
                _bookServiceMock.Verify(s => s.AddBook(It.IsAny<Book>()), Times.Once);
                _bookServiceMock.Verify(s => s.SaveChanges(), Times.Once);
            }


            [Test]
            public void AddBook_ExistingBookTitle_ReturnsBadRequest()
            {
                // Arrange
                var bookToAdd = new CompositeBookDet
                {
                    Title = "Book Title",
                    Author = "Author Name",
                    ISBN = "1234567890",
                    Image = "image-url",
                    Description = "Book Description",
                    PageCount = 200,
                    Publisher = "Publisher Name",
                    Language = "English",
                    Categories = new List<Category> { new Category { Id = Guid.NewGuid(), Name = "Category 1" } }
                };

                _bookServiceMock.Setup(s => s.GetBookMappedByTitle(bookToAdd.Title)).Returns(new Book());

                // Act
                var result = _bookController.AddBook(bookToAdd);

                // Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.AreEqual("Book already exists", badRequestResult.Value);
            }

            [Test]
            public void AddBook_MissingFields_ReturnsBadRequest()
            {
                // Arrange
                var bookToAdd = new CompositeBookDet
                {
                    Title = null,
                    Author = null,
                    ISBN = null,
                    Image = "image-url",
                    Description = "Book Description",
                    PageCount = 200,
                    Publisher = "Publisher Name",
                    Language = "English",
                    Categories = new List<Category> { new Category { Id = Guid.NewGuid(), Name = "Category 1" } }
                };

                // Act
                var result = _bookController.AddBook(bookToAdd);

                // Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.AreEqual("Please fill all the fields", badRequestResult.Value);
            }

            [Test]
            public void AddBook_NoCategoriesSelected_ReturnsBadRequest()
            {
                // Arrange
                var bookToAdd = new CompositeBookDet
                {
                    Title = "Book Title",
                    Author = "Author Name",
                    ISBN = "1234567890",
                    Image = "image-url",
                    Description = "Book Description",
                    PageCount = 200,
                    Publisher = "Publisher Name",
                    Language = "English",
                    Categories = new List<Category>()
                };

                // Act
                var result = _bookController.AddBook(bookToAdd);

                // Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.AreEqual("Please select at least one category", badRequestResult.Value);
            }

            [Test]
            public async Task GetAllBooks_ReturnsOkResultWithAllBooks()
            {
                // Arrange
                var books = new List<Book>
        {
            new Book { Id = Guid.NewGuid(), Title = "Book 1" },
            new Book { Id = Guid.NewGuid(), Title = "Book 2" },
            new Book { Id = Guid.NewGuid(), Title = "Book 3" }
        };

                _bookServiceMock.Setup(s => s.GetAllBooks()).ReturnsAsync(books);
                _bookDetailsServiceMock.Setup(s => s.GetBookDetailsById(It.IsAny<Guid>())).Returns(new BookDetails());
                _categoryServiceMock.Setup(s => s.GetByBookId(It.IsAny<Guid>())).Returns(new List<Category>());
                _reviewServiceMock.Setup(s => s.GetReviewsByBookId(It.IsAny<Guid>())).Returns(new List<Review>());

                // Act
                var result = await _bookController.GetAllBooks();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okResult = (OkObjectResult)result;
                var returnedBooks = (List<Book>)okResult.Value;
                Assert.AreEqual(books.Count, returnedBooks.Count);
                CollectionAssert.AreEqual(books, returnedBooks);

                _bookServiceMock.Verify(s => s.GetAllBooks(), Times.Once);
                _bookDetailsServiceMock.Verify(s => s.GetBookDetailsById(It.IsAny<Guid>()), Times.Exactly(books.Count));
                _categoryServiceMock.Verify(s => s.GetByBookId(It.IsAny<Guid>()), Times.Exactly(books.Count));
                _reviewServiceMock.Verify(s => s.GetReviewsByBookId(It.IsAny<Guid>()), Times.Exactly(books.Count));
            }

            [Test]
            public async Task GetBookById_ExistingBookId_ReturnsOkResultWithBook()
            {
                // Arrange
                var bookId = Guid.NewGuid();
                var book = new Book { Id = bookId, Title = "Book Title" };

                _bookServiceMock.Setup(s => s.GetBookById(bookId)).ReturnsAsync(book);
                _bookDetailsServiceMock.Setup(s => s.GetBookDetailsById(bookId)).Returns(new BookDetails());
                _categoryServiceMock.Setup(s => s.GetByBookId(bookId)).Returns(new List<Category>());
                _reviewServiceMock.Setup(s => s.GetReviewsByBookId(bookId)).Returns(new List<Review>());

                // Act
                var result = await _bookController.GetBookById(bookId);

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okResult = (OkObjectResult)result;
                var returnedBook = (Book)okResult.Value;
                Assert.IsNotNull(returnedBook);
                Assert.AreEqual(book.Id, returnedBook.Id);
                Assert.AreEqual(book.Title, returnedBook.Title);

                _bookServiceMock.Verify(s => s.GetBookById(bookId), Times.Once);
                _bookDetailsServiceMock.Verify(s => s.GetBookDetailsById(bookId), Times.Once);
                _categoryServiceMock.Verify(s => s.GetByBookId(bookId), Times.Once);
                _reviewServiceMock.Verify(s => s.GetReviewsByBookId(bookId), Times.Once);
            }

            [Test]
            public async Task GetBookById_NonExistingBookId_ReturnsNotFound()
            {
                // Arrange
                var bookId = Guid.NewGuid();

                _bookServiceMock.Setup(s => s.GetBookById(bookId)).ReturnsAsync(null as Book);

                // Act
                var result = await _bookController.GetBookById(bookId);

                // Assert
                Assert.IsInstanceOf<NotFoundResult>(result);

                _bookServiceMock.Verify(s => s.GetBookById(bookId), Times.Once);
                _bookDetailsServiceMock.Verify(s => s.GetBookDetailsById(It.IsAny<Guid>()), Times.Never);
                _categoryServiceMock.Verify(s => s.GetByBookId(It.IsAny<Guid>()), Times.Never);
                _reviewServiceMock.Verify(s => s.GetReviewsByBookId(It.IsAny<Guid>()), Times.Never);
            }

            [Test]
            public async Task DeleteBookAsync_ExistingBookId_ReturnsOkResult()
            {
                // Arrange
                var bookId = Guid.NewGuid();
                var book = new Book { Id = bookId, Title = "Book Title" };

                _bookServiceMock.Setup(s => s.GetBookById(bookId)).ReturnsAsync(book);
                _bookServiceMock.Setup(s => s.DeleteBook(book)).Verifiable();
                _bookServiceMock.Setup(s => s.SaveChanges()).Verifiable();

                // Act
                var result = await _bookController.DeleteBookAsync(bookId);

                // Assert
                Assert.IsInstanceOf<OkResult>(result);

                _bookServiceMock.Verify(s => s.GetBookById(bookId), Times.Once);
                _bookServiceMock.Verify(s => s.DeleteBook(book), Times.Once);
                _bookServiceMock.Verify(s => s.SaveChanges(), Times.Once);
            }

            [Test]
            public async Task DeleteBookAsync_NonExistingBookId_ReturnsNotFound()
            {
                // Arrange
                var bookId = Guid.NewGuid();

                _bookServiceMock.Setup(s => s.GetBookById(bookId)).ReturnsAsync(null as Book);

                // Act
                var result = await _bookController.DeleteBookAsync(bookId);

                // Assert
                Assert.IsInstanceOf<NotFoundResult>(result);

                _bookServiceMock.Verify(s => s.GetBookById(bookId), Times.Once);
                _bookServiceMock.Verify(s => s.DeleteBook(It.IsAny<Book>()), Times.Never);
                _bookServiceMock.Verify(s => s.SaveChanges(), Times.Never);
            }
        }
    }
}
