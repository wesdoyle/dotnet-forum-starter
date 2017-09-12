using Forum.Web.Models.Post;
using System.Collections.Generic;

namespace Forum.Web.Models.Home
{
    public class HomeIndexModel
    {
        public IEnumerable<ForumListingPostModel> LatestPosts { get; set; }
    }
}
