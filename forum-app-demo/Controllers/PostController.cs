using Forum.Data;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Post;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int forumId)
        {
            var model = new NewPostModel();
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

        public IActionResult Delete()
        {
            return View();
        }
    }
}