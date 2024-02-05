using Application.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public bool AddUser(User user)
        {
            _applicationDbContext.Users.Add(user);
             int affectRows = _applicationDbContext.SaveChanges();
            return affectRows > 0;
        }

        public List<User> GetAll()
        {
            return _applicationDbContext.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _applicationDbContext
                .Users
                .FirstOrDefault(c => c.Id == id);
        }

        public bool IsUserExist(int id, string email)
        {
            return _applicationDbContext.Users
                .Any(c => c.Id != id && c.Email.Equals(email));  
        }

        public bool UpdateUser(User user)
        {
            var entry = _applicationDbContext.Users.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                _applicationDbContext.Users.Attach(user);
            }

            entry.State = EntityState.Modified;
            int affectedRows = _applicationDbContext.SaveChanges();
            return affectedRows > 0;
        }
    }
}
