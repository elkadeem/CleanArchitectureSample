using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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

        public List<User> GetUsers()
        {            
            _logger.LogDebug("Calling method {methodname}", nameof(GetUsers));
            return _usersRepository.GetAll();
        }

        public User GetUser(int id)
        {
            _logger.LogDebug("Calling method {methodname} with {id}", nameof(GetUser), id);
            return _usersRepository.GetUser(id);
        }

        public void AddUser(User user)
        {
            if (user is null)
            {
                _logger.LogDebug($"{nameof(AddUser)} is null");
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                _logger.LogError("name is empty");
                throw new ArgumentOutOfRangeException(nameof(user.Name)
                    , "User name can't be null or empty."); 
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                _logger.LogError("Email is empty");
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email can't be null or empty.");
            }

            if(_usersRepository.IsUserExist(0, user.Email))
            {
                _logger.LogError("Email exists for another user.");
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email is exist for another user.");
            }

            _usersRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var currentUser = _usersRepository.GetUser(user.Id);
            if (user == null)
                throw new ArgumentException($"There is no user with id {user.Id}"
                    , nameof(user));

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ArgumentOutOfRangeException(nameof(user.Name)
                    , "User name can't be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email can't be null or empty.");
            }

            if(user.Email.EndsWith("@cbo.om"))
            {

            }

            if (_usersRepository.IsUserExist(user.Id, user.Email))
            {
                throw new ArgumentOutOfRangeException(nameof(user.Email)
                    , "Email is exist for another user.");
            }

            currentUser.Email = user.Email;
            _usersRepository.UpdateUser(currentUser);
        }
    }
}
