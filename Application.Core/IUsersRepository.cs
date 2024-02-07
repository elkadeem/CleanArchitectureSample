using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Core
{
    public interface IUsersRepository
    {        
        Task<(List<User> Items, int TotalItemsCount)> GetAsync(string userName
            , string name
            , string email
            , bool? isActive
            , int pageIndex = 0
            , int pageSize = 10);
        
        Task<User> GetAsync(int id);
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> IsUserEmailExistAsync(int userId, string email);
        Task<bool> IsUserNameExistAsync(int userId, string userName);
    }
}
