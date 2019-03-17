using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksUse.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksUse.Controllers
{
    public class YearsController : Controller
    {
        private readonly BooksUseContext _context;

        public YearsController(BooksUseContext context)
        {
            _context = context;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.user = StartController._currentUser; //Add whatever
            base.OnActionExecuting(filterContext);
        }

        // GET: Years
        public async Task<IActionResult> Index()
        {
            return View(await _context.Years.ToListAsync());
        }

        // GET: Years/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var years = await _context.Years
                .FirstOrDefaultAsync(m => m.Id == id);
            if (years == null)
            {
                return NotFound();
            }

            return View(years);
        }

        // GET: Years/Create
        public async Task<IActionResult> Create()
        {
            var lastYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync();
            ViewBag.year = lastYear.Title + 1;
            return View();
        }

        // POST: Years/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Open")] Years years)
        {
            var allYears = _context.Years;
            foreach (var year in allYears)
            {
                year.Open = false;
                _context.Update(year);
            }
            await _context.SaveChangesAsync();

            var lastYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync();
            var requests = _context.Requests.Include(r => r.FkBooksNavigation).Include(r => r.FkUsersNavigation).Include(r => r.FkYearsNavigation).Where(r => r.FkYears == lastYear.Id && r.Approved == 1);

            var newYear = new Years { Open = true, Title = lastYear.Title + 1 };
            _context.Add(newYear);
            await _context.SaveChangesAsync();

            foreach (var el in requests)
            {
                var newEl = new Requests { Approved = 0, FkBooks = el.FkBooks, FkUsers = el.FkUsers, FkYears = newYear.Id };
                _context.Add(newEl);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                _context.Add(years);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(years);
        }

        // GET: Years/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var years = await _context.Years.FindAsync(id);
            if (years == null)
            {
                return NotFound();
            }
            return View(years);
        }

        public async Task<IActionResult> Close(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var years = await _context.Years.FindAsync(id);
            if (years == null)
            {
                return NotFound();
            }

            years.Open = false;
            _context.Update(years);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // POST: Years/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Open")] Years years)
        {
            if (id != years.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(years);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YearsExists(years.Id))
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
            return View(years);
        }

        // GET: Years/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var years = await _context.Years
                .FirstOrDefaultAsync(m => m.Id == id);
            if (years == null)
            {
                return NotFound();
            }

            return View(years);
        }

        // POST: Years/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var years = await _context.Years.FindAsync(id);
            _context.Years.Remove(years);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YearsExists(int id)
        {
            return _context.Years.Any(e => e.Id == id);
        }
    }
}
