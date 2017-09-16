using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
using Forum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Forum.Tests
{

    [TestFixture]
    [Category("Services")]
    public class PostServiceTests
    {
        [Test]
        public async Task Create_Post_Creates_New_Post_Via_Context()
        {
            //var mockSet = new Mock<DbSet<Post>>();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Post_Writes_Post_To_Database").Options;

            // run the test against one instance of the context
            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);

                var post = new Post
                {
                    Title = "writing functional javascript",
                    Content = "some post content"
                };

                await postService.Add(post);
            }

            // use a separate instance of the context to verify corect data was saved to the db
            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.AreEqual(1, ctx.Posts.CountAsync().Result);
                Assert.AreEqual("writing functional javascript", ctx.Posts.SingleAsync().Result.Title);
            }
        }

        [Test]
        public void Get_Post_By_Id_Returns_Correct_Post()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id_Db").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post { Id = 1986, Title = "first post" });
                ctx.Posts.Add(new Post { Id = 223, Title = "second post" });
                ctx.Posts.Add(new Post { Id = 12, Title = "third post" });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetById(223);
                Assert.AreEqual(result.Title, "second post");
            }
        }

        [Test]
        public void Get_All_Posts_Returns_All_Posts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Post_By_Id_Db").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post { Id = 21341, Title = "first post" });
                ctx.Posts.Add(new Post { Id = 8144, Title = "second post" });
                ctx.Posts.Add(new Post { Id = 1245, Title = "third post" });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetAll();
                Assert.AreEqual(3, result.Count());
            }
        }

        [Test]
        public async Task Checking_Reply_Count_Returns_Number_Of_Replies()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Searh_Database").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post
                {
                    Id = 21341,
                });

                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var post = postService.GetById(21341);

                await postService.AddReply(new PostReply
                {
                    Post = post,
                    Content = "Here's a post reply."
                });
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var replyCount = postService.GetReplyCount(21341);
                Assert.AreEqual(replyCount, 1);
            }
        }
    }
}
