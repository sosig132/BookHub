using Back_End.Helper.JwtUtils;
using Back_End.Models.DTOs;
using Back_End.Models.Users;
using Back_End.Repositories.UserRepository;
using Microsoft.Identity.Client;
using System.ComponentModel;

namespace Back_End.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public IJwtUtils _jwtUtils;
        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }
        public User GetUserMappedByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            
            return user;
        }

        public User GetUserMappedByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            
            return user;
        }
        public User GetUserMappedById(Guid id)
        {
            var user = _userRepository.GetById(id);
            
            return user;
        }
        public User AddUser(User user)
        {
            _userRepository.Create(user);
            return user;
        }
        public bool SaveChanges()
        {
            return _userRepository.Save();
        }
        public UserResponseDTO Authenticate(UserRequestDTO model) {
            var user = _userRepository.GetByUsername(model.Username);
            if (user == null||!BCrypt.Net.BCrypt.Verify(model.Password, user.Password)) return null;
            
            var token = _jwtUtils.GenerateJwtToken(user);
            return new UserResponseDTO(user, token);
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }

}
