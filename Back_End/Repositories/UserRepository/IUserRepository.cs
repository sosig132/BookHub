using Back_End.Models.Users;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByEmail(string email);
        User GetByUsername(string username);
        User GetById(Guid id);

    }
}
