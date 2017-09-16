using System.Linq;
using Forum.Data;
using Forum.Data.Models;
using Forum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Forum.Tests
{
    [TestFixture]
    [Category("Services")]
    public class ForumServiceTests
    {
        [Test]
        public void Filtered_Posts_Returns_Correct_Result_Count()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Search_Database").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Data.Models.Forum()
                {
                    Id = 19
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = 21341,
                    Title = "Functional programming",
                    Content = "Does anyone have experience deploying Haskell to production?" 
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(19),
                    Id = -324,
                    Title = "Haskell Tail Recursion",
                    Content = "Haskell Haskell" 
                });

                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var forumService = new ForumService(ctx, postService);
                var postCount = forumService.GetFilteredPosts(19, "Haskell").Count();
                Assert.AreEqual(2, postCount);
            }
        }
    }
}
