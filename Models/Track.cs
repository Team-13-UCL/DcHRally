using DcHRally.Areas.Identity.Data;

namespace DcHRally.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public required int CategoryId { get; set; }
        public string? Name { get; set; }
        public virtual required ApplicationUser User { get; set; }
        public required string TrackData { get; set; }
    }
}
