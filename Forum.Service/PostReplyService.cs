using Forum.Data;
using System;
using Forum.Data.Models;
using System.Threading.Tasks;
using forum_app_demo.Data;

namespace Forum.Service
{
    public class PostReplyService : IPostReply
    {
        private ApplicationDbContext _context;

        public PostReplyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(int id, string message)
        {
            throw new NotImplementedException();
        }

        public PostReply GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
