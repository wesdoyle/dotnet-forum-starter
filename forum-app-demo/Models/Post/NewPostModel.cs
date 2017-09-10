using System;

namespace Forum.Web.Models.Post
{
    public class NewPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ForumName { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
