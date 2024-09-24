using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eActivities.Data;
using eActivities.Models;
using Microsoft.AspNetCore.Authorization;
using Windows.UI.Xaml.Controls;
using eActivities.ViewModels;

namespace eActivities.Controllers
{
    [Authorize]
    public class ActivityitemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityitemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Activityitems
        public async Task<IActionResult> Index(DateTime? begindate, DateTime? enddate, string Searchterm="")
        {
            var _user = User.Identity.Name;
            var ListCat = new List<Category>();
            if(_user.Equals("i.kimari@afdb.org") || _user.Equals("admin.finance@afdb.org"))
            {
                if (begindate == null || enddate == null)
                {
                    ListCat = _context.Category
                        .Include(a => a.Activities)
                        .Include(a => a.Activities.Where(s=>s.ActivityName.Contains(Searchterm) || s.Description.Contains(Searchterm) || s.AppUser.Contains(Searchterm)))
                        .OrderBy(i => i.Id).ToList();
                }
                else
                {
                    ListCat = _context.Category
                        .Include(a => a.Activities)
                        .Include(a => a.Activities.Where(a => a.DateDebut >= begindate && a.DateFin <= enddate || a.ActivityName.Contains(Searchterm) || a.Description.Contains(Searchterm) || a.AppUser.Contains(Searchterm))).OrderBy(i => i.Id).ToList();
                }
            }
            else 
            {
                if (begindate == null || enddate == null)
                {
                    ListCat = _context.Category
                        .Include(a => a.Activities)
                        .Include(a => a.Activities.Where(r=>r.AppUser.Equals(_user)))
                        .OrderBy(i => i.Id).ToList();
                }
                else
                {
                    ListCat = _context.Category
                        .Include(a => a.Activities)
                        .Include(a => a.Activities.Where(a => a.DateDebut >= begindate && a.DateFin <= enddate && a.AppUser.Equals(_user))).OrderBy(i => i.Id).ToList();
                }
            }
            ViewBag.Categories = ListCat;
            //var applicationDbContext = _context.Activityitem
            //    .Include(a => a.Category)
            //    .Include(a => a.Genre);
            return View();
        }

        // GET: Activityitems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityitem = await _context.Activityitem
                .Include(a => a.Category)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityitem == null)
            {
                return NotFound();
            }

            return View(activityitem);
        }

        // GET: Activityitems/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName");
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName");
            return View();
        }

        // POST: Activityitems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActivityName,Description,GenreId,CategoryId,DateDebut,DateFin,AppUser")] Activityitem activityitem)
        {
            if (ModelState.IsValid)
            {
                activityitem.AppUser = User.Identity.Name;
                _context.Add(activityitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", activityitem.CategoryId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", activityitem.GenreId);
            return View(activityitem);
        }

        // GET: Activityitems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityitem = await _context.Activityitem.FindAsync(id);
            if (activityitem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", activityitem.CategoryId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", activityitem.GenreId);
            return View(activityitem);
        }

        // POST: Activityitems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActivityName,Description,GenreId,CategoryId,DateDebut,DateFin,AppUser")] Activityitem activityitem)
        {
            if (id != activityitem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    activityitem.AppUser = User.Identity.Name;
                    _context.Update(activityitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityitemExists(activityitem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", activityitem.CategoryId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", activityitem.GenreId);
            return View(activityitem);
        }

        // GET: Activityitems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityitem = await _context.Activityitem
                .Include(a => a.Category)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityitem == null)
            {
                return NotFound();
            }

            return View(activityitem);
        }

        // POST: Activityitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityitem = await _context.Activityitem.FindAsync(id);
            if (activityitem != null)
            {
                _context.Activityitem.Remove(activityitem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityitemExists(int id)
        {
            return _context.Activityitem.Any(e => e.Id == id);
        }
    }
}
