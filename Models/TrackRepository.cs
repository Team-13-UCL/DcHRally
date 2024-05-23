﻿using DcHRally.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace DcHRally.Models
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DcHRallyIdentityDbContext _dbContext;
        public TrackRepository(DcHRallyIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Track> AllTracks
        {
            get
            {
                return _dbContext.Tracks.Include(t => t.UserId);
            }
        }

        public Track? GetTrackById(int trackId)
        {
            return _dbContext.Tracks.FirstOrDefault(t => t.TrackId == trackId);
        }
    }
}
