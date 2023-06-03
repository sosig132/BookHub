using Back_End.Enums;
using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.DTOs
{
    public class UserRequestDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
       
    }
}
