using System;

namespace Forum.Web.Helpers
{
    public static class PostFormatter
    {
        public static string Prettify(string postContent)
        {
            var postWithSpaces = postContent.Replace(Environment.NewLine, "<br />");
            var postFormattedLead = postWithSpaces.Replace("[code]", "<pre>");
            var postFormattedTail = postFormattedLead.Replace(@"[/code]", "</pre>");
            return postFormattedTail;
        }

    }
}
