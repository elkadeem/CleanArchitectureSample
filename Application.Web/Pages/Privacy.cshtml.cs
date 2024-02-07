using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Web.Pages
{
    [Authorize(Policy = "WaelPolicy")]
    [Authorize(Policy = "AdminPolicy")]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogWarning($"User '{User.Identity.Name}' is trying to access the application.");
        }
    }

}
