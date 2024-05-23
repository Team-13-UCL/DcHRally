using DcHRally.Areas.Identity.Data;
using DcHRally.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RallyBaneTest.Models;
using RallyBaneTest.ViewModels;
using System.Diagnostics;

namespace RallyBaneTest.Controllers;

[Authorize]
public class TrackController : Controller
{
    private IObstacleRepository _obstacleRepository;
    private ICategoryRepository _categoryRepository;
    private IObstacleElementRepository _obstacleElementRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly DcHRallyIdentityDbContext _context;


    public TrackController(IObstacleRepository obstacleRepository, ICategoryRepository categoryRepository, IObstacleElementRepository obstacleElementRepository, UserManager<ApplicationUser> userManager, DcHRallyIdentityDbContext context)
    {
        _obstacleRepository = obstacleRepository;
        _categoryRepository = categoryRepository;
        _obstacleElementRepository = obstacleElementRepository;
        _userManager = userManager;
        _context = context;
    }

    public IActionResult Index(string category)
    {
        IEnumerable<Obstacle> obstacles;
        IEnumerable<ObstacleElement> obstacleElements;
        string? currentCategory;

        obstacleElements = _obstacleElementRepository.AllObstacleElements;
        if (string.IsNullOrEmpty(category))
        {
            obstacles = _obstacleRepository.AllObstacles.OrderBy(o => o.ObstacleId);
            currentCategory = "All obstacles";
        }
        else
        {
            obstacles = _obstacleRepository.AllObstacles.Where(o => o.Category.Name == category)
                .OrderBy(o => o.ObstacleId);
            currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.Name == category)?.Name;
        }

        return View(new ObstacleViewModel(obstacles, currentCategory, obstacleElements));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> SaveTrack([FromBody] TrackDto trackDto)
    {
        if (trackDto == null)
        {
            return BadRequest("Track JSON is null");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        if (trackDto.TrackData == null)
        {
            return BadRequest("TrackData is missing");
        }

        var dtoCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.Name == trackDto.Category);
        if (dtoCategory == null)
        {
            return BadRequest("Invalid category");
        }

        var track = new Track
        {
            Name = trackDto.Name,
            CategoryId = dtoCategory.CategoryId,
            TrackData = trackDto.TrackData,
            User = user
        };

        _context.Tracks.Add(track);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}