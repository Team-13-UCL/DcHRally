using Microsoft.EntityFrameworkCore;
using RallyBaneTest.Models;

namespace DcHRally.Models
{
    public class TrackRepository : ITrackRepository
    {
        private readonly RallyDbContext _rallyDbContext;
        public TrackRepository(RallyDbContext rallyDbContext)
        {
            _rallyDbContext = rallyDbContext;
        }
        public IEnumerable<Track> AllTracks
        {
            get
            {
                return _rallyDbContext.Tracks.Include(t => t.User);
            }
        }

        public Track? GetTrackById(int trackId)
        {
            return _rallyDbContext.Tracks.FirstOrDefault(t => t.TrackId == trackId);
        }
    }
}
