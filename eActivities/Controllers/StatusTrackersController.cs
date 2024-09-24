using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eActivities.Data;
using eActivities.Models;
using Microsoft.AspNetCore.Authorization;

namespace eActivities.Controllers
{
    [Authorize]
    public class StatusTrackersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusTrackersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusTrackers
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusTrackers.ToListAsync());
        }

        // GET: StatusTrackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusTracker = await _context.StatusTrackers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusTracker == null)
            {
                return NotFound();
            }

            return View(statusTracker);
        }

        // GET: StatusTrackers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusTrackers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusTrackerName")] StatusTracker statusTracker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusTracker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusTracker);
        }

        // GET: StatusTrackers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusTracker = await _context.StatusTrackers.FindAsync(id);
            if (statusTracker == null)
            {
                return NotFound();
            }
            return View(statusTracker);
        }

        // POST: StatusTrackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusTrackerName")] StatusTracker statusTracker)
        {
            if (id != statusTracker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusTracker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusTrackerExists(statusTracker.Id))
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
            return View(statusTracker);
        }

        // GET: StatusTrackers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusTracker = await _context.StatusTrackers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusTracker == null)
            {
                return NotFound();
            }

            return View(statusTracker);
        }

        // POST: StatusTrackers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusTracker = await _context.StatusTrackers.FindAsync(id);
            if (statusTracker != null)
            {
                _context.StatusTrackers.Remove(statusTracker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusTrackerExists(int id)
        {
            return _context.StatusTrackers.Any(e => e.Id == id);
        }
    }
}
