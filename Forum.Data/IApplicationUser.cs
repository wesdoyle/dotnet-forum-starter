using Forum.Data.Models;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);

        Task IncrementRating(string id);
        Task Add(ApplicationUser user);
        Task Deactivate(ApplicationUser user);
    }
}
