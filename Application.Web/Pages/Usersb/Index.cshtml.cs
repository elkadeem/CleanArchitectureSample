using Application.Core;
using Application.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace Application.Web.Pages.Usersb
{
    public class IndexModel : PageModel
    {
        private readonly UsersService _usersService;
        private readonly ILogger<IndexModel> _logger;
        private const int PageSize = 1;
        public IndexModel(UsersService usersService
            , ILogger<IndexModel> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        public IPagedList<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            PageNumber = PageNumber <= 0 ? 1 : PageNumber;
            var result = await _usersService.GetAsync(Username
                , Name, Email
                , null
                , PageNumber
                , PageSize);

            Users = new StaticPagedList<User>(result.Items, PageNumber
                , PageSize, result.TotalItemsCount);
            return Page();
        }
    }
}
