using System;
using System.Linq;
using System.Threading.Tasks;
using App.Areas.Identity.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Authorize(Policy = "RequireAdmin")]
    public class FeaturesController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public FeaturesController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Features
        public async Task<IActionResult> Index(int categoryId)
        {
            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Title");
            var features = from f in _context.Feature select f;

            if (categoryId != 0) features = features.Where(s => s.CategoryId == categoryId);

            return View(await features
                .Include(m => m.Category)
                .ToListAsync());
        }

        // GET: Features/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var feature = await _context.Feature
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null) return NotFound();

            return View(feature);
        }

        // GET: Features/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Title");
            return View();
        }

        // POST: Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate,CategoryId")]
            Feature feature)
        {
            if (ModelState.IsValid)
            {
                feature.CreatedAt = DateTime.Now;
                feature.VoteCount = 0;
                _context.Add(feature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return Create();
        }

        // GET: Features/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var feature = await _context.Feature.FindAsync(id);
            if (feature == null) return NotFound();

            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Title");
            return View(feature);
        }

        // POST: Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,CategoryId")]
            Feature feature)
        {
            if (id != feature.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeatureExists(feature.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return await Edit(id);
        }

        // GET: Features/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var feature = await _context.Feature
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null) return NotFound();

            return View(feature);
        }

        // POST: Features/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feature = await _context.Feature.FindAsync(id);
            _context.Feature.Remove(feature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeatureExists(int id)
        {
            return _context.Feature.Any(e => e.Id == id);
        }
    }
}