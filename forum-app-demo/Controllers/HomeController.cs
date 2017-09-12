using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using forum_app_demo.Models;
using Forum.Web.Models.Home;
using Forum.Data;
using Forum.Web.Models.Post;

namespace forum_app_demo.Controllers
{
    public class HomeController : Controller
    {
        private IForum _forumService;

        protected HomeController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        public HomeIndexModel BuildHomeIndexModel()
        {
            var posts = _forumService.GetLatestPosts(10);

            var latest = posts.Select(post => new ForumListingPostModel { });

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
