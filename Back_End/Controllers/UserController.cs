using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Back_End.Data;
using Back_End.Models.Users;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context; 

        public UserController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetUserById([FromQuery] Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user object");
            }

            if (_context.Users.Any(u => u.Email == user.Email)){
                return Conflict("Email already exists");
            }

            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return Conflict("Username already exists");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);



            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }
    }
}
