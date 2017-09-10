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

        public Task Add(Post post)
        {
            throw new NotImplementedException();
        }

        public Task Archive(Post post)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Post post)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(Post post, string content)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetById(int id)
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
