using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.Pages.Users
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Country { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
