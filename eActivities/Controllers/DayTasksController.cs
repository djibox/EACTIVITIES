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
    public class DayTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string _currentUser;

        public DayTasksController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: DayTasks
        public async Task<IActionResult> Index(DateTime? begindate, DateTime? enddate, bool export = false)
        {
            _currentUser = User.Identity.Name;

            if (begindate == null || enddate == null)
            {
                if (_currentUser.Equals("i.kimari@afdb.org") || _currentUser.Equals("admin.finance@afdb.org"))
                {
                    var applicationDbContext = _context.DayTask.Include(d => d.ResponsibleManager).Include(d => d.StatusTracker);
                    return View(await applicationDbContext.ToListAsync());
                }
                else
                {
                    var applicationDbContext = _context.DayTask.Include(d => d.ResponsibleManager).Include(d => d.StatusTracker).Where(d => d.CreatedBy.Equals(_currentUser));
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            else
            {
                if (_currentUser.Equals("i.kimari@afdb.org") || _currentUser.Equals("admin.finance@afdb.org"))
                {
                    var applicationDbContext = _context.DayTask.Include(d => d.ResponsibleManager).Include(d => d.StatusTracker).Where(c => c.InitialTargetDate >= begindate && c.RevisedDate <= enddate);
                    return View(await applicationDbContext.ToListAsync());
                }
                else
                {
                    var applicationDbContext = _context.DayTask.Include(d => d.ResponsibleManager).Include(d => d.StatusTracker).Where(c => c.InitialTargetDate >= begindate && c.RevisedDate <= enddate && c.CreatedBy.Equals(_currentUser));
                    return View(await applicationDbContext.ToListAsync());
                }
            }
        }

        // GET: DayTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var dayTask = await _context.DayTask
                .Include(d => d.ResponsibleManager)
                .Include(d => d.StatusTracker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayTask == null)
            {
                return NotFound();
            }

            return View(dayTask);
        }

        // GET: DayTasks/Create
        public IActionResult Create()
        {
            ViewData["ResponsibleManagerId"] = new SelectList(_context.ResponsibleManagers, "Id", "FullName");
            ViewData["StatusTrackerId"] = new SelectList(_context.StatusTrackers, "Id", "StatusTrackerName");
            return View();
        }

        // POST: DayTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskAction,ResponsibleManagerId,StatusTrackerId,InitialTargetDate,RevisedDate,Comments,CreatedBy")] DayTask dayTask)
        {
            if (ModelState.IsValid)
            {
                _currentUser = User.Identity.Name;
                dayTask.CreatedBy = _currentUser;
                _context.Add(dayTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResponsibleManagerId"] = new SelectList(_context.ResponsibleManagers, "Id", "FullName", dayTask.ResponsibleManagerId);
            ViewData["StatusTrackerId"] = new SelectList(_context.StatusTrackers, "Id", "StatusTrackerName", dayTask.StatusTrackerId);
            return View(dayTask);
        }

        // GET: DayTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTask = await _context.DayTask.FindAsync(id);
            if (dayTask == null)
            {
                return NotFound();
            }
            ViewData["ResponsibleManagerId"] = new SelectList(_context.ResponsibleManagers, "Id", "FullName", dayTask.ResponsibleManagerId);
            ViewData["StatusTrackerId"] = new SelectList(_context.StatusTrackers, "Id", "StatusTrackerName", dayTask.StatusTrackerId);
            return View(dayTask);
        }

        // POST: DayTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskAction,ResponsibleManagerId,StatusTrackerId,InitialTargetDate,RevisedDate,Comments,CreatedBy")] DayTask dayTask)
        {
            if (id != dayTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var _myName = User.Identity.Name;
                    dayTask.CreatedBy = _myName;
                    _context.Update(dayTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayTaskExists(dayTask.Id))
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
            ViewData["ResponsibleManagerId"] = new SelectList(_context.ResponsibleManagers, "Id", "FullName", dayTask.ResponsibleManagerId);
            ViewData["StatusTrackerId"] = new SelectList(_context.StatusTrackers, "Id", "StatusTrackerName", dayTask.StatusTrackerId);
            return View(dayTask);
        }

        // GET: DayTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayTask = await _context.DayTask
                .Include(d => d.ResponsibleManager)
                .Include(d => d.StatusTracker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayTask == null)
            {
                return NotFound();
            }

            return View(dayTask);
        }

        // POST: DayTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayTask = await _context.DayTask.FindAsync(id);
            if (dayTask != null)
            {
                _context.DayTask.Remove(dayTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayTaskExists(int id)
        {
            return _context.DayTask.Any(e => e.Id == id);
        }
    }
}
