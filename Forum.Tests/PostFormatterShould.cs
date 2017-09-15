using System;
using Forum.Web.Helpers;
using NUnit.Framework;

namespace Forum.Tests
{
    [TestFixture]
    public class PostFormatterShould
    {
        private readonly IPostFormatter _postFormatter;

        public PostFormatterShould(IPostFormatter postFormatter)
        {
            _postFormatter = postFormatter;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            Console.WriteLine("Starting");
        }

        [Test]
        public void PreFormat_Strings_With_Code_Tags()
        {
            const string input = "Here's a post [code] with code [/code] in it.";
            const string expected = "Here's a post <pre> with code </pre> in it.";

            var output = _postFormatter.Prettify(input);

            Assert.AreEqual(expected, output);

            var staticType = typeof(PostFormatter);
            var ci = staticType.TypeInitializer;
            var parameters = new object[0];
            ci.Invoke(null, parameters);
        }
    }
}
