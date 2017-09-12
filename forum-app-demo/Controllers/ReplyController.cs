using Microsoft.AspNetCore.Mvc;
using Forum.Data;
using Forum.Web.Models.Reply;
using Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Post(int id)
        {
            var post = _postService.GetById(id);
            var forum = _forumService.GetById(post.Forum.Id);
            var replyingUser = GetReplyingUser();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
                 
            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = id.ToString(),

                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,

                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorId = user.Id,
                AuthorRating = user.Rating,

                Date = DateTime.Now
            };

            return View(model);
        }

        private async Task<ApplicationUser> GetReplyingUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}