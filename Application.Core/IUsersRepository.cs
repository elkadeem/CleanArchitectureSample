using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        User GetUser(int id);
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool IsUserExist(int userId, string email);
    }
}
