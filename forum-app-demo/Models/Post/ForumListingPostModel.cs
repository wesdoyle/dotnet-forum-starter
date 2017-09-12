using System;

namespace Forum.Web.Models.Post
{
    public class ForumListingPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int AuthorRating { get; set; }
        public string DatePosted { get; set; }
        public int RepliesCount { get; set; }
        public string ForumImageUrl { get; set; }
    }
}
