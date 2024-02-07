using Application.Core;
using Application.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Web.Pages.Usersb
{
    public class CreateModel : PageModel
    {
        private readonly UsersService _usersService;
        private readonly ILogger<CreateModel> _logger;
        
        public CreateModel(UsersService usersService
            , ILogger<CreateModel> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [BindProperty]
        public UserViewModel UserViewModel { get; set; }

        public void OnGet()
        {
            UserViewModel = new UserViewModel();
            FillLookups(UserViewModel);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User(0, UserViewModel.Username, UserViewModel.Name, UserViewModel.Email)
                    {
                        Country = UserViewModel.Country,
                        Active = UserViewModel.IsActive,
                    };

                    if (UserViewModel.Roles != null
                        && UserViewModel.Roles.Count > 0)
                    {
                        user.Roles.AddRange(UserViewModel.Roles);
                    }

                    _logger.LogInformation("Creating user {0}", user.UserName);
                    await _usersService.AddAsync(user);
                    _logger.LogInformation("User {0} created", user.UserName);
                    TempData["Message"] = "User created successfully";
                    return RedirectToPage("./");
                }

                _logger.LogError("Invalid model state");
                ViewData["Message"] = "Invalid model state";

            }
            catch (ArgumentOutOfRangeException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                _logger.LogError("Invalid model state");
                ViewData["Message"] = "Invalid model state";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                ViewData["Message"] = "Error creating user";
            }


            FillLookups(UserViewModel);
            return Page();
        }

        private static void FillLookups(UserViewModel model)
        {
            model.Countries = new List<SelectListItem> {
                new SelectListItem { Text = "USA", Value = "USA" },
                new SelectListItem { Text = "Canada", Value = "Canada" },
                new SelectListItem { Text = "Mexico", Value = "Mexico" }
            };

            model.RoleList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "User", Value = "User" },
                new SelectListItem { Text = "Guest", Value = "Guest" }
            }; ;
        }
    }
}
