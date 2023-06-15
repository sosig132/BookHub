using System;
using System.Threading.Tasks;
using Back_End.Controllers;
using Back_End.Models.Categories;
using Back_End.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Back_End.Tests.Controllers
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private CategoryController _categoryController;
        private Mock<ICategoryService> _categoryServiceMock;

        [SetUp]
        public void Setup()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);
        }

        [Test]
        public async Task GetAllCategories_ReturnsOkResult()
        {
            // Arrange
            var expectedCategories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Category 1" },
                new Category { Id = Guid.NewGuid(), Name = "Category 2" }
            };
            _categoryServiceMock.Setup(service => service.GetAllCategories()).Returns(Task.FromResult(expectedCategories));

            // Act
            var result = await _categoryController.GetAllCategories();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(expectedCategories));
        }

        [Test]
        public void AddCategory_WithValidCategory_ReturnsOkResult()
        {
            // Arrange
            var category = new Category { Name = "New Category" };
            _categoryServiceMock.Setup(service => service.GetByName(category.Name)).Returns((Category)null);

            // Act
            var result = _categoryController.AddCategory(category);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(category, okResult.Value);
            _categoryServiceMock.Verify(service => service.AddUser(category), Times.Once);
            _categoryServiceMock.Verify(service => service.SaveChanges(), Times.Once);
        }

        [Test]
        public void AddCategory_WithNullCategory_ReturnsBadRequest()
        {
            // Act
            var result = _categoryController.AddCategory(null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void AddCategory_WithNullCategoryName_ReturnsBadRequest()
        {
            // Arrange
            var category = new Category { Name = null };

            // Act
            var result = _categoryController.AddCategory(category);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void AddCategory_WithExistingCategoryName_ReturnsBadRequest()
        {
            // Arrange
            var category = new Category { Name = "Existing Category" };
            _categoryServiceMock.Setup(service => service.GetByName(category.Name)).Returns(category);

            // Act
            var result = _categoryController.AddCategory(category);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetCategoryById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var expectedCategory = new Category { Id = categoryId, Name = "Category 1" };
            _categoryServiceMock.Setup(service => service.GetById(categoryId)).Returns(expectedCategory);

            // Act
            var result = _categoryController.GetCategoryById(categoryId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedCategory, okResult.Value);
        }

        [Test]
        public void GetCategoryById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _categoryServiceMock.Setup(service => service.GetById(categoryId)).Returns((Category)null);

            // Act
            var result = _categoryController.GetCategoryById(categoryId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}