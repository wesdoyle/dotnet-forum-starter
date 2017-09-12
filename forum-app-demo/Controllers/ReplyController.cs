using Microsoft.AspNetCore.Mvc;
using Forum.Data;
using Forum.Web.Models.Reply;
using Forum.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum.Web.Controllers
{
    public class ReplyController : Controller
    {
        private IForum _forumService;
        private IPost _postService;
        private IApplicationUser _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        protected ReplyController(IForum forumService, IPost postService, IApplicationUser userService, UserManager<ApplicationUser> userManager)
        {
            _forumService = forumService;
            _postService = postService;
            _userService = userService;
            _userManager = userManager;
        }

        public IActionResult Post(int id)
        {
            var post = _postService.GetById(id);
            var forum = _forumService.GetById(post.Forum.Id);

            var model = new PostReplyModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };

            return View(model);
        }
    }
}