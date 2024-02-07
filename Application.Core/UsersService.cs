using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class UsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUsersRepository usersRepository
            , ILogger<UsersService> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public Task<(List<User> Items, int TotalItemsCount)> GetAsync(string userName
            , string name
            , string email
            , bool? isActive
            , int page = 1
            , int pageSize = 10)
        {            
            int pageIndex = page<= 0? 0 : page - 1;
            return _usersRepository.GetAsync(userName, name, email, isActive, pageIndex, pageSize);
        }

        public Task<User> GetAsync(int id)
        {
            _logger.LogDebug("Calling method {methodname} with {id}", nameof(GetAsync), id);
            return _usersRepository.GetAsync(id);
        }

        public async Task AddAsync(User user)
        {
            if (user is null)
            {               
                throw new ArgumentNullException(nameof(user));
            }
            
            if(await _usersRepository.IsUserEmailExistAsync(0, user.Email))
            {
                _logger.LogError("Email exists for another user.");
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email is exist for another user.");
            }

            if (await _usersRepository.IsUserNameExistAsync(0, user.UserName))
            {
                _logger.LogError("Email exists for another user.");
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email is exist for another user.");
            }

            await _usersRepository.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var currentUser = await _usersRepository.GetAsync(user.Id);
            if (currentUser == null)
            {
                throw new ArgumentException($"There is no user with id {user.Id}"
                    , nameof(user));
            }

            if (await _usersRepository.IsUserEmailExistAsync(user.Id, user.Email))
            {
                _logger.LogError("Email exists for another user.");
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email is exist for another user.");
            }

            if (await _usersRepository.IsUserNameExistAsync(user.Id, user.UserName))
            {
                _logger.LogError("Email exists for another user.");
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email is exist for another user.");
            }

            currentUser.UpdateUser(user.UserName, user.Name, user.Email);
            currentUser.Country = user.Country;
            currentUser.Active = user.Active;
            await _usersRepository.UpdateAsync(currentUser);
        }

        
    }
}
