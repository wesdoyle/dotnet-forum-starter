using System;

namespace Forum.Web.Models.Forum
{
    public class ForumListingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfUsers { get; set; }
        public DateTime LatestActivity { get; set; }
    }
}
