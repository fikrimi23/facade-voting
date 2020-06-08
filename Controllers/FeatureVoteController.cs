using System.Linq;
using System.Threading.Tasks;
using App.Areas.Identity.Data;
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
        public IActionResult Upvote(int? id)
        {
            throw new System.NotImplementedException();
        }
        
        [Authorize]
        public IActionResult Downvote(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}