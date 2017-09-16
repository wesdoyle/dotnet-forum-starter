using System;
using Forum.Web.Helpers;
using NUnit.Framework;

namespace Forum.Tests
{
    [Category("Post Formatter")]
    [TestFixture]
    public class PostFormatterShould
    {
        [Test]
        public void Replace_Env_NewLine_With_Br_Tag()
        {
            var input = $"Here is a post {Environment.NewLine} with a newline.";
            var expected = $"Here is a post <br /> with a newline.";
            var pf = new PostFormatter();
            var output = pf.Prettify(input);
            Assert.AreEqual(output, expected);
        }

        [TestCase("[code]Some code[/code]","<pre>Some code</pre>")]
        [TestCase("Here is [code]some code[/code]","Here is <pre>some code</pre>")]
        [TestCase("[code]Here[/code] is [code]some code[/code]","<pre>Here</pre> is <pre>some code</pre>")]
        public void Replace_Custom_Code_Tags_With_Pre_Tags(string input, string expected)
        {
            var pf = new PostFormatter();
            var output = pf.Prettify(input);
            Assert.AreEqual(output, expected);
        }

        [TestCase("No code here", "No code here")]
        [TestCase("[Code]No code here[/Code]", "[Code]No code here[/Code]")]
        [TestCase("[No code here]", "[No code here]")]
        [TestCase("<pre>No code here</pre>", "<pre>No code here</pre>")]
        public void Not_Change_String_Without_Code_Tags(string input, string expected)
        {
            var pf = new PostFormatter();
            var output = pf.Prettify(input);
            Assert.AreEqual(output, expected);
        }
    }
}
