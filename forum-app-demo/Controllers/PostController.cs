using Forum.Data;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private IPost _postService;

        public PostController(IPost postService)
        {
            _postService = postService;
        }

        [Authorize]
        public IActionResult Create(int forumId)
        {
            var model = new NewPostModel();
            return View(model);
        }

        [Authorize]
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

        [Authorize]
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
        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            var post = _postService.GetById(id);
            _postService.Delete(id);

            return RedirectToAction("Index", "Forum", new { id = post.Forum.Id });
        }
    }
}