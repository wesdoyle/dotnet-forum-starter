using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
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
                Latest = GetLatestPost(f.Id) ?? new PostListingModel(),
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

        public PostListingModel GetLatestPost(int forumId)
        {
            var post = _forumService.GetLatestPost(forumId);

            if(post != null)
            {
                return new PostListingModel
                {
                    Author = post.User != null ? post.User.UserName : "",
                    DatePosted = post.Created.ToString(CultureInfo.InvariantCulture),
                    Title = post.Title ?? ""
                };
            }

            return new PostListingModel();
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

        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new {id, searchQuery});
        }

        public IActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumService.GetById(id);
            var posts = _forumService.GetFilteredPosts(id, searchQuery).ToList();
            var noResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Forum = BuildForumListing(post),
                Author = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(CultureInfo.InvariantCulture),
                RepliesCount = post.Replies.Count()
            }).OrderByDescending(post=>post.DatePosted);

            var model = new TopicResultModel
            {
                EmptySearchResults = noResults,
                Posts = postListings,
                SearchQuery = searchQuery,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }

        private static ForumListingModel BuildForumListing(Data.Models.Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                ImageUrl = forum.ImageUrl,
                Name = forum.Title,
                Description = forum.Description
            };
        }

        private static ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);
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
    }
}