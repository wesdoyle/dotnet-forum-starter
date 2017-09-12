using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Forum.Data.Models;
using Forum.Web.Models.ApplicationUser;
using Forum.Data;

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

        public IActionResult Index(string id)
        {
            var user = _userService.GetById(id);

            var model = new ProfileModel()
            {
                UserId = user.Id,
                Username = user.UserName,
                Description = user.UserDescription,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl
            };

            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}