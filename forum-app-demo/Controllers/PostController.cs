using Forum.Data;
using Microsoft.AspNetCore.Mvc;

namespace forum_app_demo.Controllers
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