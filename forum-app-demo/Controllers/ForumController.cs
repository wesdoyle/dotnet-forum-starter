using Forum.Data;
using Forum.Web.Models.ApplicationUser;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                Id = f.Id,
                Name = f.Title,
                Description = f.Description,
                NumberOfPosts = f.Posts?.Count() ?? 0,
                LatestPost = GetLatestPost(f.Id) ?? new ForumListingPostModel(),
                NumberOfUsers = _forumService.GetActiveUsers(f.Id).Count(),
                ImageUrl = f.ImageUrl,
                HasRecentPost = _forumService.HasRecentPost(f.Id) 
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
            var post = _forumService.GetLatestPost(forumId);

            if(post != null)
            {
                return new ForumListingPostModel
                {
                    Author = post.User != null ? post.User.UserName : "",
                    DatePosted = post.Created != null ? post.Created.ToString() : "",
                    Title = post.Title ?? ""
                };
            }

            return new ForumListingPostModel();
        }

        public IEnumerable<ApplicationUserModel> GetActiveUsers(int forumId)
        {
            return _forumService.GetActiveUsers(forumId).Select(appUser => new ApplicationUserModel
            {
                Id = Convert.ToInt32(appUser.Id),
                ProfileImageUrl = appUser.ProfileImageUrl,
                Rating = appUser.Rating,
                Username = appUser.UserName
            });
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);

            var allPosts = forum.Posts.Select(post => new ForumListingPostModel
            {
                Id = post.Id,
                Author = post.User.UserName,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count()
            });

            var latestPost = allPosts
                .OrderByDescending(post => post.DatePosted)
                .FirstOrDefault();

            var model = new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                AllPosts = allPosts,
                ImageUrl = forum.ImageUrl,
                LatestPost = latestPost,
                NumberOfPosts = allPosts.Count(),
                NumberOfUsers = _forumService.GetActiveUsers(id).Count()
            };

            return View(model);
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