using System;
using System.Collections.Generic;

namespace Forum.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsArchived { get; set; }

        public virtual IEnumerable<PostReply> Replies { get; set; }
        public virtual Forum Forum { get; set; }
    }
}
