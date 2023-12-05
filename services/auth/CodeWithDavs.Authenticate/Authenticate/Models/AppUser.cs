using Microsoft.AspNetCore.Identity;

namespace Authenticate.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string ProfileUrl { get; set; }
        public Address Address { get; set; }
    }
}
