using Microsoft.AspNetCore.Identity;

namespace forum_app_demo.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserDescription { get; set; }
    }
}
