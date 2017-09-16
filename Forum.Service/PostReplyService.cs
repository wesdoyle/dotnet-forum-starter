using Forum.Data;
using Forum.Data.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Forum.Service
{
    public class PostReplyService : IPostReply
    {
        private readonly ApplicationDbContext _context;

        public PostReplyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PostReply GetById(int id)
        {
            return _context.PostReplies
                .Include(r=>r.Post)
                .ThenInclude(post=>post.Forum)
                .Include(r=>r.Post)
                .ThenInclude(post => post.User).First(r => r.Id == id);
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
