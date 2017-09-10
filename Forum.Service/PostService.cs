using Forum.Data;
using System;
using System.Collections.Generic;
using Forum.Data.Models;
using System.Threading.Tasks;
using forum_app_demo.Data;

namespace Forum.Service
{
    public class PostService : IPost
    {
        private ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task Archive(int id)
        {
            var post = GetById(id);
            post.IsArchived = true;
            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = GetById(id);
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public Task EditPostContent(int id, string content)
        {
            throw new NotImplementedException();
        }

        public Post GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsByForumId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetReplyCount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
