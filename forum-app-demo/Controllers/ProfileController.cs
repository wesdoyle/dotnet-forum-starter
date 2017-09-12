using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Forum.Data.Models;
using Forum.Web.Models.ApplicationUser;
using Forum.Data;
using System.Net.Http.Headers;
using System.IO;

namespace Forum.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IApplicationUser _userService;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _userManager = userManager;
            _userService = userService;
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

        [HttpPost]
        public IActionResult UploadImage(ProfileModel model)
        {
            var filename = ContentDispositionHeaderValue
                                    .Parse(model.ImageUpload.ContentDisposition)
                                    .FileName
                                    .Trim('"');
            filename = Path.Combine("/Content/UserProfile/", $@"\{filename}");

            if (Directory.Exists("/Content/UserProfile/"))
            {
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    model.ImageUpload.CopyTo(fs);
                    fs.Flush();
                }
            }

            model.ProfileImageUrl = "~/Content/UserImages/" + model.ImageUpload.FileName;

            return View();
        }
    }
}