namespace Forum.Web.Models.Forum
{
    public class TopicResultModel
    {
        public int ForumId { get; set; }
        public ForumListingModel Forum { get; set; }
        public string SearchQuery { get; set; }
    }
}
