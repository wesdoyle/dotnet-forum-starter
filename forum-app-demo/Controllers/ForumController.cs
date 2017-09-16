using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Service;
using Forum.Web.Models.ApplicationUser;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Forum.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ForumController(IForum forumService, IConfiguration configuration, IApplicationUser userService, IUpload uploadService)
        {
            _forumService = forumService;
            _configuration = configuration;
            _userService = userService;
            _uploadService = uploadService;
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

            var forumListingModels = forums as IList<ForumListingModel> ?? forums.ToList();

            var model = new ForumIndexModel
            {
                ForumList = forumListingModels.OrderBy(forum=>forum.Name),
                NumberOfForums = forumListingModels.Count()
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
                    DatePosted = post.Created.ToString(CultureInfo.InvariantCulture),
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

        public IActionResult Topic(int id, TopicResultModel topicModel = null)
        {
            var forum = _forumService.GetById(id);
            var posts = _forumService.GetFilteredPosts(id, topicModel?.SearchQuery).ToList();

            var postListings = posts.Select(post => new ForumListingPostModel
            {
                Id = post.Id,
                Author = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(CultureInfo.InvariantCulture),
                RepliesCount = post.Replies.Count()
            }).OrderByDescending(post=>post.DatePosted);

            var latestPost = postListings 
                .OrderByDescending(post => post.DatePosted)
                .FirstOrDefault();

            var count = postListings.Count();

            var forumListing = new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                AllPosts = postListings,
                ImageUrl = forum.ImageUrl,
                LatestPost = latestPost,
                NumberOfPosts = count,
                NumberOfUsers = _forumService.GetActiveUsers(forum.Id).Count()
            };

            var model = new TopicResultModel 
            {
                Forum = forumListing,
                ForumId = forum.Id,
                SearchQuery = topicModel?.SearchQuery 
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new AddForumModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumModel model)
        {

            var imageUri = "";

            if (model.ImageUpload != null)
            {
                var blockBlob = PostForumImage(model.ImageUpload);
                imageUri = blockBlob.Uri.AbsoluteUri;
            }

            else
            {
                imageUri = "/images/users/default.png";
            }

            var forum = new Data.Models.Forum()
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                ImageUrl = imageUri
            };

            await _forumService.Add(forum);
            return RedirectToAction("Index", "Forum");
        }

        public CloudBlockBlob PostForumImage(IFormFile file)
        {
            var connectionString = _configuration.GetConnectionString("AzureStorageAccountConnectionString");
            var container = _uploadService.GetBlobContainer(connectionString);
            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var filename = Path.Combine(parsedContentDisposition.FileName.ToString().Trim('"'));
            var blockBlob = container.GetBlockBlobReference(filename);
            blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return blockBlob;
        }

        [HttpPost]
        public IActionResult Search(TopicResultModel model)
        {
            _forumService.GetFilteredPosts(model.ForumId, model.SearchQuery);
            return RedirectToAction("Topic", new {id = model.ForumId, model});
        }
    }
}