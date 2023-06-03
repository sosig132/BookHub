using Back_End.Models.Users;

namespace Back_End.Helper.JwtUtils
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid ValidateJwtToken(string token);

    }
}
