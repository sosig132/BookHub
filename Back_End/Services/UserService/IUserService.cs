using Back_End.Models.DTOs;
using Back_End.Models.Users;

namespace Back_End.Services.UserService
{
    public interface IUserService
    {
        User GetUserMappedByEmail(string email);
        User GetUserMappedByUsername(string username);
        User GetUserMappedById(Guid id);
        User AddUser(User user);
        bool SaveChanges();
        UserResponseDTO Authenticate(UserRequestDTO model);
        Task<List<User>> GetAllUsers();
    }
}
