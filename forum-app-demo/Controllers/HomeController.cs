using System.Diagnostics;
using System.Linq;
using Forum.Data;
using Forum.Data.Models;
using Forum.Web.Models.Home;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postService;

        public HomeController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        public HomeIndexModel BuildHomeIndexModel()
        {
            var latest = _postService.GetLatestPosts(10);

            var posts = latest.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                Author = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = _postService.GetReplyCount(post.Id),
                ForumName = post.Forum.Title,
                ForumImageUrl = _postService.GetForumImageUrl(post.Id),
                ForumId = post.Forum.Id
            });

            return new HomeIndexModel()
            {
                LatestPosts = posts
            };

        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Topic", "Forum", new { searchQuery });
        }
    }
}
