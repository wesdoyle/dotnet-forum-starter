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

        public PostReply GetById(int id)
        {
            return _context.PostReplies.Find(id);
        }

        public async Task Delete(int id)
        {
            var reply = GetById(id);
            _context.Remove(reply);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, string message)
        {
            var reply = GetById(id);
            await _context.SaveChangesAsync();
            _context.Update(reply);
            await _context.SaveChangesAsync();
        }
    }
}
