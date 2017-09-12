using System;

namespace Forum.Web.Models.Reply
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public int AuthorRating { get; set; }
        public DateTime Date { get; set; }
        public string ReplyContent { get; set; }
    }
}
