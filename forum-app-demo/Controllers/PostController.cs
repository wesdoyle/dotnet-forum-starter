using Forum.Data;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private IPost _postService;
        private IForum _forumService;

        public PostController(IPost postService, IForum forumService)
        {
            _postService = postService;
            _forumService = forumService;
        }

        public IActionResult Create(int forumId)
        {
            var forum = _forumService.GetById(forumId);
            var model = new NewPostModel
            {
                ForumName = forum.Title;
            };
            return View(model);
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