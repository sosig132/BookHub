using Back_End.Enums;
using Back_End.Models.Users;

namespace Back_End.Models.DTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string DisplayName { get; set; }
        public bool? IsBanned { get; set; }
        public string Token { get; set; }

        public UserResponseDTO(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Username = user.Username;
            Role = user.Role;
            DisplayName = user.DisplayName;
            IsBanned = user.IsBanned;
            Token = token;
        }
    }
}
