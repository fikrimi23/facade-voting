using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Areas.Identity.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class FeatureVoteController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public FeatureVoteController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var features = from f in _context.Feature select f;

            return View(await features
                .Include(m => m.Category)
                .OrderBy(m => m.VoteCount)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var feature = await _context.Feature
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null) return NotFound();

            return View(feature);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upvote(int? id)
        {
            if (id == null) return NotFound();

            var feature = await _context.Feature.FindAsync(id);
            if (feature == null) return NotFound();
// Check features -> users() exists

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userVotes = from uv in _context.UserVote select uv;
            var exists = userVotes
                .Where(uv => uv.FeatureId == id)
                .Any(u => u.UserId == userId);

            if (!exists)
            {
                feature.VoteCount++;
                var userVote = new UserVote
                {
                    FeatureId = feature.Id,
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _context.Add(userVote);
                _context.Update(feature);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public Task<IActionResult> Downvote(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}