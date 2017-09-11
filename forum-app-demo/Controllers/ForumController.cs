using Forum.Data;
using Forum.Web.Models.ApplicationUser;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
                Name = f.Title,
                Description = f.Description,
                NumberOfPosts = f.Posts?.Count() ?? 0,
                LatestPost = GetLatestPost(f.Id) ?? new ForumListingPostModel(),
                NumberOfUsers = _forumService.GetActiveUsers(f.Id)?.Count() ?? 0
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

            return new ForumListingPostModel
            {
                Author = post.User != null ? post.User.UserName : "", 
                DatePosted = post.Created != null ? post.Created.ToString() : "",
                Title = post.Title ?? "" 
            };
        }

        public IEnumerable<ApplicationUserModel> GetActiveUsers(int forumId)
        {
            return _forumService.GetActiveUsers(forumId).Select(appUser => new ApplicationUserModel {
                Id = Convert.ToInt32(appUser.Id),
                ProfileImageUrl = appUser.ProfileImageUrl,
                Rating = appUser.Rating,
                Username = appUser.UserName
            });
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