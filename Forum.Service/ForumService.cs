using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
using forum_app_demo.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Forum.Service
{
    public class ForumService : IForum
    {
        private ApplicationDbContext _context;
        private IPost _postService;

        public ForumService(ApplicationDbContext context, IPost postService)
        {
            _context = context;
            _postService = postService;
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
            var posts = GetById(forumId).Posts;

            if(posts == null || !posts.Any())
            {
                return new List<ApplicationUser>();
            }

            return _postService.GetAllUsers(posts);
        }

        public IEnumerable<Data.Models.Forum> GetAll()
        {
            return _context.Forums
                .Include(forum=>forum.Posts);
        }

        public Data.Models.Forum GetById(int id)
        {
            var forum = _context.Forums
                .Where(f => f.Id == id)
                .Include(f=>f.Posts)
                .First();

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
