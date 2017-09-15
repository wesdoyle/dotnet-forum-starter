using System;

namespace Forum.Web.Helpers
{
    public class PostFormatter : IPostFormatter
    {
        public string Prettify(string postContent)
        {
            var postWithSpaces = postContent.Replace(Environment.NewLine, "<br />");
            var postFormattedLead = postWithSpaces.Replace("[code]", "<pre>");
            var postFormattedTail = postFormattedLead.Replace(@"[/code]", "</pre>");
            return postFormattedTail;
        }
    }
}
