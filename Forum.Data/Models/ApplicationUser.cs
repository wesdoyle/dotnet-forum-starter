using Microsoft.AspNetCore.Identity;

namespace Forum.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserDescription { get; set; }
    }
}
