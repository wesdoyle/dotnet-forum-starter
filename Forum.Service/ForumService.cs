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
            var forum = GetById(forumId);

            var activeUsers = new List<ApplicationUser>();
            var postingUsers = forum.Posts?.Select(p => p.User).Distinct() ?? new List<ApplicationUser>();
            var replyingUsers = forum.Posts?.SelectMany(p => p.Replies).Select(r => r.User).Distinct() ?? new List<ApplicationUser>();

            activeUsers.AddRange(postingUsers);
            activeUsers.AddRange(replyingUsers);

            return activeUsers.Distinct();
        }

        public IEnumerable<Data.Models.Forum> GetAll()
        {
            return _context.Forums;
        }

        public Data.Models.Forum GetById(int id)
        {
            var forum = _context.Forums.Find(id);

            if(forum.Posts == null)
            {
                forum.Posts = new List<Post>();
            }

            return forum;
        }

        public Post GetLatestPost(int forumId)
        {
            var posts = GetById(forumId).Posts;

            if(posts != null)
            {
                return GetById(forumId).Posts
                    .OrderByDescending(post => post.Created)
                    .FirstOrDefault();
            }

            return new Post();
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
