using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using forum_app_demo.Models;
using Forum.Web.Models.Home;
using Forum.Data;
using Forum.Web.Models.Post;
using System.Linq;

namespace forum_app_demo.Controllers
{
    public class HomeController : Controller
    {
        private IPost _postService;

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

            var posts = latest.Select(post => new ForumListingPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Author = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = _postService.GetReplyCount(post.Id),
                ForumName = post.Forum.Title,
                ForumImageUrl = _postService.GetForumImageUrl(post.Id)
            }).OrderByDescending(post=>post.DatePosted);

            return new HomeIndexModel()
            {
                LatestPosts = posts
            };

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
