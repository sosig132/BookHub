using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Back_End.Data;
using Back_End.Models.Users;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Back_End.Services.UserService;
using Back_End.Enums;
using Back_End.Models.Reviews;
using Back_End.Models.DTOs;
using Back_End.Helper.Attributes;
using System.Diagnostics;
using Back_End.Services.ReviewService;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public UserController(IUserService userService, IReviewService reviewService)
        {
            _userService= userService;
            _reviewService = reviewService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            foreach(var user in users)
            {
                var reviews = _reviewService.GetReviewsByUserId(user.Id);
                user.Reviews = reviews;
            }
            return Ok(users);
        }

        [HttpGet("byId")]
        public IActionResult GetUserById([FromQuery] Guid id)
        {
            var user = _userService.GetUserMappedById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var reviews = _reviewService.GetReviewsByUserId(user.Id);

            user.Reviews = reviews;
            return Ok(user);
        }

        [HttpGet("byUsername/{username}")]
        public IActionResult GetUserByUsername( string username)
        {
            var user = _userService.GetUserMappedByUsername(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var reviews = _reviewService.GetReviewsByUserId(user.Id);
            user.Reviews = reviews;
            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] User user)
        {
            user.DateCreated = DateTime.Now;
            user.Id= Guid.NewGuid();
            user.Role = Role.User;
            user.IsBanned = false;
            user.Reviews = new List<Review>();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            if (user == null)
            {
                return BadRequest("Invalid user object");
            }

            if (_userService.GetUserMappedByEmail(user.Email)!=null){
                return Conflict("Email already exists");
            }

            if (_userService.GetUserMappedByUsername(user.Username)!=null)
            {
                return Conflict("Username already exists");
            }




            _userService.AddUser(user);
            _userService.SaveChanges();

            return Ok(user);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserRequestDTO user)
        {
            var userResponse = _userService.Authenticate(user);
            if (userResponse == null)
            {
                return BadRequest("Invalid username or password");
            }
            return Ok(userResponse);
        }
        [Authorization(Role.Admin)]
        [HttpGet("admin")]
        public IActionResult GetAdmin()
        {
            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        [Authorization(Role.User)]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok("User");
        }

        [Authorization(Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user object");
            }

            var userToUpdate = _userService.GetUserMappedById(id);
            if (userToUpdate == null)
            {
                return NotFound("User not found");
            }
            userToUpdate.IsBanned = user.IsBanned;
            userToUpdate.DateModified = DateTime.Now;
            _userService.UpdateUser(userToUpdate);
            _userService.SaveChanges();

            return Ok(userToUpdate);
        }

    }
}
