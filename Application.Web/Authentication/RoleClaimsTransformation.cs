using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Application.Web.Authentication
{
    public class RoleClaimsTransformation : IClaimsTransformation
    {
        private readonly AllowedRolesOptions _allowedRolesOptions;
        private readonly IConfiguration _configuration;

        public RoleClaimsTransformation(IOptions<AllowedRolesOptions> allowedRolesOptions
            , IConfiguration configuration)
        {
            _allowedRolesOptions = allowedRolesOptions.Value;
            _configuration = configuration;
        }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var allowedUsers = _configuration.GetSection("Authentication:AllowedUsers").Get<List<string>>();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();            
            if (allowedUsers.Contains(principal.Identity.Name))
            {
                foreach(var role in _allowedRolesOptions.AllowedRoles) {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}
