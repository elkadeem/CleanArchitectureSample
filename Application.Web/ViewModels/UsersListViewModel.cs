using Application.Core;
using X.PagedList;

namespace Application.Web.ViewModels
{
    public class UsersListViewModel
    {
        public string Username { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public int Page { get; set; }

        public IPagedList<User> Users { get; set; }
    }
}
