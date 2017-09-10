using System.Collections.Generic;

namespace Forum.Web.Models.Forum
{
    public class ForumIndexModel
    {
        public int NumberOfForums { get; set; }
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}
