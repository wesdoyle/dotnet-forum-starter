using System.Collections.Generic;
using Forum.Web.Models.Post;

namespace Forum.Web.Models.Forum
{
    public class TopicResultModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResults { get; set; }
    }
}
