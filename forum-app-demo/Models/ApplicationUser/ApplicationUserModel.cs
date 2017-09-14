namespace Forum.Web.Models.ApplicationUser
{
    public class ApplicationUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProfileImageUrl { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
    }
}
