using System;

namespace Forum.Web.Models.Post
{
    public class ForumListingPostModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string DatePosted { get; set; }
        public int RepliesCount { get; set; }
    }
}
