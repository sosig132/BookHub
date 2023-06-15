using Back_End.Controllers;
using Back_End.Enums;
using Back_End.Models.Reviews;
using Back_End.Models.Users;
using Back_End.Models.DTOs;
using Back_End.Services.ReviewService;
using Back_End.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _userController;
        private Mock<IUserService> _userServiceMock;
        private Mock<IReviewService> _reviewServiceMock;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _reviewServiceMock = new Mock<IReviewService>();
            _userController = new UserController(_userServiceMock.Object, _reviewServiceMock.Object);
        }

        [Test]
        public async Task GetAllUsers_ReturnsOkResultWithUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "user1", Email = "user1@example.com", DisplayName = "User 1" },
                new User { Id = Guid.NewGuid(), Username = "user2", Email = "user2@example.com", DisplayName = "User 2" },
                new User { Id = Guid.NewGuid(), Username = "user3", Email = "user3@example.com", DisplayName = "User 3" }
            };
            _userServiceMock.Setup(s => s.GetAllUsers()).ReturnsAsync(users);
            _reviewServiceMock.Setup(s => s.GetReviewsByUserId(It.IsAny<Guid>())).Returns(new List<Review>());

            // Act
            var result = await _userController.GetAllUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var returnedUsers = (List<User>)okResult.Value;
            Assert.AreEqual(users.Count, returnedUsers.Count);
            CollectionAssert.AreEqual(users, returnedUsers);
        }

        [Test]
        public void GetUserById_ValidId_ReturnsOkResultWithUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Username = "user1", Email = "user1@example.com", DisplayName = "User 1" };
            _userServiceMock.Setup(s => s.GetUserMappedById(userId)).Returns(user);
            _reviewServiceMock.Setup(s => s.GetReviewsByUserId(userId)).Returns(new List<Review>());

            // Act
            var result = _userController.GetUserById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public void GetUserByUsername_ValidUsername_ReturnsOkResultWithUser()
        {
            // Arrange
            var username = "user1";
            var user = new User { Id = Guid.NewGuid(), Username = username, Email = "user1@example.com", DisplayName = "User 1" };
            _userServiceMock.Setup(s => s.GetUserMappedByUsername(username)).Returns(user);
            _reviewServiceMock.Setup(s => s.GetReviewsByUserId(user.Id)).Returns(new List<Review>());

            // Act
            var result = _userController.GetUserByUsername(username);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public void Register_ValidUser_ReturnsOkResultWithUser()
        {
            // Arrange
            var user = new User
            {
                Username = "user1",
                Password = "password",
                Email = "user1@example.com",
                DisplayName = "User 1",
                Role = Role.User
            };
            _userServiceMock.Setup(s => s.GetUserMappedByEmail(user.Email)).Returns((User)null);
            _userServiceMock.Setup(s => s.GetUserMappedByUsername(user.Username)).Returns((User)null);

            // Act
            var result = _userController.Register(user);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(user, okResult.Value);
            _userServiceMock.Verify(s => s.AddUser(user), Times.Once);
            _userServiceMock.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task Login_ValidUser_ReturnsOkResultWithUserResponse()
        {
            // Arrange
            var userRequest = new UserRequestDTO { Username = "user1", Password = "password" };
            var user = new User { Id = Guid.NewGuid(), Username = userRequest.Username, Email = "user1@example.com", DisplayName = "User 1", Role = Role.User };
            var token = "sample_token";
            var userResponse = new UserResponseDTO(user, token);

            _userServiceMock.Setup(s => s.Authenticate(userRequest)).Returns(userResponse);

            // Act
            var result = await _userController.Login(userRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userResponse, okResult.Value);
        }


        [Test]
        public void GetUser_ValidRequest_ReturnsOkResultWithString()
        {
            // Arrange

            // Act
            var result = _userController.GetUser();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var value = (string)okResult.Value;
            Assert.AreEqual("User", value);
        }

        [Test]
        public void UpdateUser_ValidIdAndUser_ReturnsOkResultWithUpdatedUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userToUpdate = new User { Id = userId, Username = "user1", Email = "user1@example.com", DisplayName = "User 1", IsBanned=false};
            var updatedUser = new User { Id = userId, Username = "user1", Email = "user1@example.com", DisplayName = "User 1", IsBanned = true };
            _userServiceMock.Setup(s => s.GetUserMappedById(userId)).Returns(userToUpdate);

            // Act
            var result = _userController.UpdateUser(userId, updatedUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var returnedUser = (User)okResult.Value;
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(updatedUser.Id, returnedUser.Id);
            Assert.AreEqual(updatedUser.Username, returnedUser.Username);
            Assert.IsNotNull(returnedUser.DateModified);
            _userServiceMock.Verify(s => s.UpdateUser(userToUpdate), Times.Once);
            _userServiceMock.Verify(s => s.SaveChanges(), Times.Once);
        }
    }
}
