using Forum.Data;
using System;
using System.Threading.Tasks;
using forum_app_demo.Data;
using Forum.Data.Models;
using System.Linq;

namespace Forum.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(ApplicationUser user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task Deactivate(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetByName(string name)
        {
            return _context.ApplicationUsers.FirstOrDefault(user => user.UserName == name);
        }

        public ApplicationUser GetById(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(user => user.Id == id);
        }

        public async Task IncrementRating(string id)
        {
            var user = GetById(id);
            user.Rating += 1;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
