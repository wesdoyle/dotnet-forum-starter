using Forum.Web.Models.Post;
using System;
using System.Collections.Generic;

namespace Forum.Web.Models.Forum
{
    public class ForumListingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfUsers { get; set; }
        public string ImageUrl { get; set; }

        public ForumListingPostModel LatestPost { get; set; }
        public IEnumerable<ForumListingPostModel> AllPosts { get; set; }
    }
}
