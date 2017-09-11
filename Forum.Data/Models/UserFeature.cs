namespace Forum.Data.Models
{
    public class UserFeature
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual FeatureType Feature { get; set; }
    }
}
