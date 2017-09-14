using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Forum.Data.Models;
using Forum.Web.Models.ApplicationUser;
using Forum.Data;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Forum.Service;
using Microsoft.Extensions.Configuration;

namespace Forum.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);

            var model = new ProfileModel()
            {
                UserId = user.Id,
                Username = user.UserName,
                UserRating = user.Rating.ToString(),
                Description = user.UserDescription,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl
            };

            return View(model);
        }

        /*
         * Files uploaded using the IFormFile technique are buffered in memory or on disk on the web server 
         * before being processed. Inside the action method, the IFormFile contents are accessible as a stream. 
         * In addition to the local file system, files can be streamed to Azure Blob storage or Entity Framework.
         */

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);
            var connectionString = _configuration.GetConnectionString("AzureStorageAccountConnectionString");
            var container = _uploadService.GetBlobContainer(connectionString);

            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var filename = Path.Combine(parsedContentDisposition.FileName.Trim('"'));

            var blockBlob = container.GetBlockBlobReference(filename);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            return RedirectToAction("Detail", "Profile", new { id = userId });
        }

        public IActionResult Index()
        {
            var model = new ProfileListModel;
            return View(model);
        }
    }
}