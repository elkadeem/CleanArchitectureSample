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

        public async Task<bool> AddAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _applicationDbContext.Users.Add(user);
            int affectedRows = await _applicationDbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<(List<User> Items, int TotalItemsCount)> GetAsync(string userName, string name, string email, bool? isActive, int pageIndex = 0, int pageSize = 10)
        {
            var query = _applicationDbContext.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                query = query.Where(u => u.UserName.Contains(userName));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u => u.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => u.Email.Contains(email));
            }

            if (isActive.HasValue)
            {
                query = query.Where(u => u.Active == isActive.Value);
            }

            int totalItemsCount = await query.CountAsync();
            List<User> items = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, totalItemsCount);
        }

        public Task<User> GetAsync(int id)
        {
            return _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<bool> IsUserEmailExistAsync(int userId, string email)
        {
            return _applicationDbContext
                .Users
                .AnyAsync(u => u.Id != userId && u.Email == email);
        }

        public Task<bool> IsUserNameExistAsync(int userId, string userName)
        {
            return _applicationDbContext.Users.AnyAsync(u => u.Id != userId
            && u.UserName == userName);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var entry = _applicationDbContext.Users.Entry(user);
            if(entry.State == EntityState.Detached)
            {
                _applicationDbContext.Users.Attach(user);
            }

            entry.State = EntityState.Modified;
            int affectedRows = await _applicationDbContext.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
