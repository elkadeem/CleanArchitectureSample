using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Application.Web.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }
        
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Country { get; set; }

        public bool IsActive { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
    }
}
