using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IPost
    {
        Task Add(Post post);
        Task Archive(Post post);
        Task Delete(Post post);
        Task EditPostContent(Post post, string content);

        int GetReplyCount(int id);

        IEnumerable<Post> GetById(int id);
        IEnumerable<Post> GetPostsByUserId(int id);
        IEnumerable<Post> GetPostsByForumId(int id);
        IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end);
    }
}
