using DcHRally.Areas.Identity.Data;
using DcHRally.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RallyBaneTest.Models;

namespace DcHRally.Controllers
{
    [Authorize]
    public class TracksOverviewController : Controller
    {
        private readonly RallyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DcHRallyIdentityDbContext _identityContext;

        public TracksOverviewController(RallyDbContext context, UserManager<ApplicationUser> userManager, DcHRallyIdentityDbContext IdentityContext)
        {
            _context = context;
            _userManager = userManager;
            _identityContext = IdentityContext;
        }

        // GET: TracksOverview
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(await _context.Tracks.Where(t => t.UserId == user.Id).ToListAsync());
        }

        // GET: TracksOverview/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .FirstOrDefaultAsync(m => m.TrackId == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: TracksOverview/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TracksOverview/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackId,CategoryId,Name,TrackData")] Track track)
        {
            if (ModelState.IsValid)
            {
                _context.Add(track);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(track);
        }

        // GET: TracksOverview/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            return View(track);
        }

        // POST: TracksOverview/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrackId,CategoryId,Name,TrackData")] Track track)
        {
            if (id != track.TrackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.TrackId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(track);
        }

        // GET: TracksOverview/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .FirstOrDefaultAsync(m => m.TrackId == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: TracksOverview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(int id)
        {
            return _context.Tracks.Any(e => e.TrackId == id);
        }
    }
}
