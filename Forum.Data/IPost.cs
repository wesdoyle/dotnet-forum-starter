using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IPost
    {
        Task Add(Post post);
        Task Archive(int id);
        Task Delete(int id );
        Task EditPostContent(int id, string content);

        int GetReplyCount(int id);

        Post GetById(int id);
        IEnumerable<Post> GetPostsByUserId(int id);
        IEnumerable<Post> GetPostsByForumId(int id);
        IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end);
    }
}
