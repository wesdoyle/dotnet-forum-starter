using Forum.Data;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;

namespace forum_app_demo.Controllers
{
    public class ForumController : Controller
    {
        private IForum _forumService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(f => new ForumListingModel
            {
                Name = f.Title,
                Description = f.Description,
                NumberOfPosts = f.Posts.Count(),
                LatestPost = GetLatestPost(f.Id),
                NumberOfUsers = GetActiveUsers(f.Id).Count()
            });

            var model = new ForumIndexModel
            {
                ForumList = forums,
                NumberOfForums = forums.Count()
            };

            return View(model);
        }

        public ForumListingPostModel GetLatestPost(int forumId)
        {

        }

        public IEnumerable<ApplicationUserListingModel> GetActiveUsers
        {

        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}