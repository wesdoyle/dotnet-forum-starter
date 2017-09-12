using Forum.Data;
using Forum.Data.Models;
using Forum.Web.Models.Post;
using Forum.Web.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private IPost _postService;
        private IForum _forumService;
        private IApplicationUser _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, IForum forumService, IApplicationUser userService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userService = userService;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);

            var replies = GetPostReplies(post);

            var model = new PostIndexModel {
                Id = post.Id,
                Title = post.Title,
                Author = post.User.UserName,
                Date = post.Created,
                AuthorRating = post.User.Rating,
                PostContent = post.Content,
                Replies = replies
            };

            return View(model);
        }

        private IEnumerable<PostReplyModel> GetPostReplies(Post post)
        {
            return post.Replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                Author = reply.User.UserName,
                AuthorRating = reply.User.Rating,
                Date = reply.Created,
                ReplyContent = reply.Content
            });
        }

        public IActionResult Create(int id)
        {
            // note id here is Forum.Id
            var forum = _forumService.GetById(id);

            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                AuthorName = User.Identity.Name,
                ForumImageUrl = _forumService.GetById(forum.Id).ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var post = BuildPost(model, user);
            await _postService.Add(post);
            return RedirectToAction("Index", "Forum", model.ForumId);
        }

        public Post BuildPost(NewPostModel post, ApplicationUser user)
        {
            var now = DateTime.Now;
            var forum = _forumService.GetById(post.ForumId);

            return new Post
            {
                Title = post.Title,
                Content = post.Content,
                Created = now,
                Forum = forum,
                User = user,
                IsArchived = false
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