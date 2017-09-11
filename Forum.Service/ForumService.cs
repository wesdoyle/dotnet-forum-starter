using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
using forum_app_demo.Data;
using System.Linq;

namespace Forum.Service
{
    public class ForumService : IForum
    {
        private ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Data.Models.Forum forum)
        {
            _context.Add(forum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var forum = GetById(id);
            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int forumId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Data.Models.Forum> GetAll()
        {
            return _context.Forums;
        }

        public Data.Models.Forum GetById(int id)
        {
            return _context.Forums.Find(id);
        }

        public Post GetLatestPost(int forumId)
        {
            return GetById(forumId).Posts
                .OrderByDescending(post => post.Created)
                .FirstOrDefault();
        }

        public async Task UpdateForumDescription(int id, string description)
        {
            var forum = GetById(id);
            forum.Description = description;

            _context.Update(forum);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateForumTitle(int id, string title)
        {
            var forum = GetById(id);
            forum.Title = title;

            _context.Update(forum);
            await _context.SaveChangesAsync();
        }
    }
}
