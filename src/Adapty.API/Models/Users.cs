using Microsoft.AspNetCore.Identity;

namespace Adapty.API.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}