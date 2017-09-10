using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IForum
    {
        Models.Forum GetById(int id);
        IEnumerable<Models.Forum> GetAll(int id);
        Task Create(Models.Forum forum);
        Task Delete(int id);
        Task UpdateForumTitle(int id, string title);
        Task UpdateForumDescription(int id, string description);
    }
}
