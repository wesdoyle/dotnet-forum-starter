using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Forum.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserDescription { get; set; }
        public string ProfileImageUrl { get; set; }
        public int Rating { get; set; }

        public virtual IEnumerable<Favorite> Favorites { get; set; }
        public virtual IEnumerable<UserFeature> AvailableFeatures { get; set; }
    }
}
