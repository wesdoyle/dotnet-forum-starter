using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IForum
    {
        IEnumerable<Models.Forum> GetAll(int id);
        Task Create(Models.Forum forum);
        Task Delete(int id);
        Task UpdateForumTitle(Models.Forum forum, string Title);
        Task UpdateForumDescription(Models.Forum forum, string Description);
    }
}
