using Forum.Data;
using Forum.Data.Models;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private IPost _postService;
        private IForum _forumService;
        private IApplicationUser _userService;

        public PostController(IPost postService, IForum forumService, IApplicationUser userService)
        {
            _postService = postService;
            _forumService = forumService;
            _userService = userService;
        }

        public IActionResult Create(int forumId)
        {
            var forum = _forumService.GetById(forumId);
            var user = User.Identity.Name;

            var model = new NewPostModel
            {
                ForumName = forum.Title
            };

            return View(model);
        }

        [HttpPost]
        public IActionResults AddPost(NewPostModel model)
        {
            var post = BuildPost(model);

            _postService.Add(post);
        }

        public Post BuildPost(NewPostModel post, ApplicationUser user)
        {
            return new Post
            {
                Title = post.Title,
            };
        }

        public IActionResult Edit(int postId)
        {
            var post = _postService.GetById(postId);

            var model = new NewPostModel
            {
                Title = post.Title,
                Content = post.Content,
                Created = post.Created,
            };

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var post = _postService.GetById(id);
            var model = new DeletePostModel
            {
                PostId = post.Id,
                PostAuthor = post.User.UserName,
                PostContent = post.Content
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var post = _postService.GetById(id);
            _postService.Delete(id);

            return RedirectToAction("Index", "Forum", new { id = post.Forum.Id });
        }
    }
}