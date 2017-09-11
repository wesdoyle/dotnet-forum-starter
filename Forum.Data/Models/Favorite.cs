using System.Collections.Generic;

namespace Forum.Data.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Forum Forum { get; set; }
    }
}
