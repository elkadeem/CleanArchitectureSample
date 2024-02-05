using Application.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Web.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UsersService _usersService;

        public CreateModel(UsersService usersService)
        {
            _usersService = usersService;
        }

        [BindProperty]
        public UserViewModel User { get; set; }
        public void OnGet()
        {
            User = new UserViewModel();
            User.Countries = new List<SelectListItem>() {
              new SelectListItem("Oman", "OM"),
              new SelectListItem("Egypt", "EG"),
              new SelectListItem("Saudi Arabia", "KSA")
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                User.Countries = new List<SelectListItem>() {
              new SelectListItem("Oman", "OM"),
              new SelectListItem("Egypt", "EG"),
              new SelectListItem("Saudi Arabia", "KSA")
            };
                return Page();
            }


            User user = new User(0, User.Name, User.Email);
            _usersService.AddUser(user);
            return RedirectToPage("./Index");
        }
    }
}
