using DcHRally.Areas.Identity.Data;
using RallyBaneTest.Models;

namespace DcHRally.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public required Category Category { get; set; }
        public string? Name { get; set; }
        public virtual required ApplicationUser User { get; set; }
        public required string TrackData { get; set; }
    }
}
