using Back_End.Enums;
using Back_End.Models.Reviews;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Back_End.Models.Users
{
    public class User : BaseEntity.BaseEntity
    {
        public required string Username { get; set; }
        [JsonIgnore]
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string DisplayName { get; set; }
        public Role Role { get; set; }
        public bool? IsBanned { get; set; }
        public ICollection<Review>? Reviews { get; set; }
      
    }
}
