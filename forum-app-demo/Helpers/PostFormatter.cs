using System;

namespace Forum.Web.Helpers
{
    public class PostFormatter : IPostFormatter
    {
        public string Prettify(string post)
        {
            var postWithSpaces = TransformSpaces(post);
            var postCodeFormatted = TransformCodeTags(postWithSpaces);
            return postCodeFormatted;
        }

        private static string TransformSpaces(string post)
        {
            return post.Replace(Environment.NewLine, "<br />");
        }

        private static string TransformCodeTags(string post)
        {
            var head = post.Replace("[code]", "<pre>");
            return head.Replace(@"[/code]", "</pre>");
        }
    }
}
