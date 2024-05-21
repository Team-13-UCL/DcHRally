using DcHRally.Areas.Identity.Data;

namespace DcHRally.Models
{
    public class User
    {
        public int UserId { get; set; }
        public required ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Track>? Tracks { get; set; }
    }
}
