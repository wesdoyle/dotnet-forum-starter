namespace Forum.Web.Models.ApplicationUser
{
    public class ProfileModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }
        public string UserRating { get; set; }
    }
}
