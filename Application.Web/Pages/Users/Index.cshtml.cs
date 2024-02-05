using Application.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UsersService _usersService;
        public IndexModel(UsersService usersService)
        {
            _usersService = usersService;
        }

        public List<User> Users { get; set; }

        public IActionResult OnGet()
        {
            Users = _usersService.GetUsers();
            return Page();
        }
    }
}
