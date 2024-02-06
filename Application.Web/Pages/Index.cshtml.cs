using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            _logger.LogWarning($"User '{User.Identity.Name}' is trying to access the application.");
            ViewData["ConnectionString"] = _configuration.GetConnectionString("DefaultConnection");

            ViewData["adminRole"] = _configuration["Roles:AdminRole"];
        }
    }
}
