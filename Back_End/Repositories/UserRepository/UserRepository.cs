using Back_End.Data;
using Back_End.Models.Users;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

        public User GetByEmail(string email)
        {
            return _table.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public User GetByUsername(string username)
        {
            return _table.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User GetById(Guid id)
        {
            return _table.FirstOrDefault(x => x.Id == id);
        }
    }
}
