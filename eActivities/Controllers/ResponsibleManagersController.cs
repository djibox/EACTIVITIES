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
    public class ResponsibleManagersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsibleManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResponsibleManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResponsibleManagers.ToListAsync());
        }

        // GET: ResponsibleManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsibleManager = await _context.ResponsibleManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsibleManager == null)
            {
                return NotFound();
            }

            return View(responsibleManager);
        }

        // GET: ResponsibleManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResponsibleManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName")] ResponsibleManager responsibleManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsibleManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsibleManager);
        }

        // GET: ResponsibleManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsibleManager = await _context.ResponsibleManagers.FindAsync(id);
            if (responsibleManager == null)
            {
                return NotFound();
            }
            return View(responsibleManager);
        }

        // POST: ResponsibleManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName")] ResponsibleManager responsibleManager)
        {
            if (id != responsibleManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsibleManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsibleManagerExists(responsibleManager.Id))
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
            return View(responsibleManager);
        }

        // GET: ResponsibleManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsibleManager = await _context.ResponsibleManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsibleManager == null)
            {
                return NotFound();
            }

            return View(responsibleManager);
        }

        // POST: ResponsibleManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsibleManager = await _context.ResponsibleManagers.FindAsync(id);
            if (responsibleManager != null)
            {
                _context.ResponsibleManagers.Remove(responsibleManager);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsibleManagerExists(int id)
        {
            return _context.ResponsibleManagers.Any(e => e.Id == id);
        }
    }
}
