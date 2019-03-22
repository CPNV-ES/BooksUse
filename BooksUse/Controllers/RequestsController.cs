using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksUse.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BooksUse.Models
{
    public class RequestsController : Controller
    {
        private readonly BooksUseContext _context;

        public RequestsController(BooksUseContext context)
        {
            _context = context;
        }

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            StartController._currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);
            ViewBag.user = StartController._currentUser; //Add whatever
            base.OnActionExecuting(filterContext);
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            StartController._currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);

            var booksUseContext = _context.Requests
                .Include(r => r.FkBooksNavigation)
                .Include(r => r.FkUsersNavigation)
                .Include(r => r.FkYearsNavigation)
                .Include(r => r.SchoolClassesRequests)
                .ThenInclude( s => s.FkSchoolClassesNavigation)
                .Where(r => r == r);

            if (StartController._currentYear != null)
            {
                booksUseContext = booksUseContext.Where(r => r.FkYears == StartController._currentYear.Id);
            }

            // Check role
            if (StartController._currentUser.FkRoles == 1)
            {
                booksUseContext = booksUseContext.Where(r => r.FkUsersNavigation.Id == StartController._currentUser.Id).OrderBy(r => r.Approved);
                return View("indexTeachers", await booksUseContext.ToListAsync());
            }
            ViewData["Name"] = "Liste des demandes";

            return View(await booksUseContext.ToListAsync());
        }

        public async Task<IActionResult> IndexPending()
        {
            StartController._currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);

            var booksUseContext = _context.Requests
                .Include(r => r.FkBooksNavigation)
                .Include(r => r.FkUsersNavigation)
                .Include(r => r.FkYearsNavigation)
                .Include(r => r.SchoolClassesRequests)
                .ThenInclude(s => s.FkSchoolClassesNavigation)
                .Where(r => r.FkYearsNavigation.Open == true);

            if (StartController._currentYear == null)
            {
                return View("Index", await booksUseContext.ToListAsync());
            }
            //Get context
            booksUseContext = booksUseContext.Where(r => r.FkYears == StartController._currentYear.Id && r.Approved == 0);

            ViewData["Name"] = "Liste des demandes en attente d'approbation";

            return View("Index", await booksUseContext.ToListAsync());
        }

        public async Task<IActionResult> Approved(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var requests = await _context.Requests.FindAsync(id);
            
            if (requests == null)
            {
                return NotFound();
            }
            requests.Approved = 1;
            _context.Update(requests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests
                .Include(r => r.FkBooksNavigation)
                .Include(r => r.FkUsersNavigation)
                .Include(r => r.FkYearsNavigation)
                .Include(r => r.SchoolClassesRequests)
                .ThenInclude(s => s.FkSchoolClassesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requests == null)
            {
                return NotFound();
            }

            return View(requests);
        }

        // GET: Requests/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SchoolClassesRequests"] = new SelectList(_context.SchoolClasses, "Id", "Name");

            if (StartController._currentUser.FkRoles == 1)
            {
                var requests = await _context.Requests.Where(r => r.FkUsers == StartController._currentUser.Id).ToListAsync();
                var idBooks = requests.Select(b => b.FkBooks).ToArray();

                var books = await _context.Books.Where(r => Array.IndexOf(idBooks, r.Id) == -1).ToListAsync();

                ViewData["FkBooks"] = new SelectList(books, "Id", "Title");
                
                return View("createTeachers");
            }
            else
            {
                ViewData["FkUsers"] = from u in _context.Users.Where(r => r.FkRoles == 1)
                                      select new SelectListItem
                                      {
                                          Value = u.Id.ToString(),
                                          Text = u.FirstName + " " + u.LastName
                                      };

                ViewData["FkBooks"] = new SelectList(_context.Books, "Id", "Title");
                return View();
            }

            
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolClassesRequests", "FkYears", "FkBooks", "FkUsers")] Requests requests)
        {
            requests.FkYears = StartController._currentYear.Id;
            requests.Approved = 1;

            if (requests.FkUsers != 0)
            {
                requests.FkUsers = requests.FkUsers;
            }
            else
            {
                requests.FkUsers = StartController._currentUser.Id;
            }

            _context.Add(requests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    _context.Add(requests);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            ////ViewData["FkBooks"] = new SelectList(_context.Books, "Id", "Author", requests.FkBooks);
            ////ViewData["FkUsers"] = new SelectList(_context.Users, "Id", "FirstName", requests.FkUsers);
            //return View(requests);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }
            ViewData["FkBooks"] = new SelectList(_context.Books, "Id", "Author", requests.FkBooks);
            ViewData["FkUsers"] = new SelectList(_context.Users, "Id", "FirstName", requests.FkUsers);
            return View(requests);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ForYear,Approved,FkUsers,FkBooks")] Requests requests)
        {
            if (id != requests.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestsExists(requests.Id))
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
            ViewData["FkBooks"] = new SelectList(_context.Books, "Id", "Author", requests.FkBooks);
            ViewData["FkUsers"] = new SelectList(_context.Users, "Id", "FirstName", requests.FkUsers);
            return View(requests);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests
                .Include(r => r.FkBooksNavigation)
                .Include(r => r.FkUsersNavigation)
                .Include(r => r.FkYearsNavigation)
                .Include(r => r.SchoolClassesRequests)
                .ThenInclude(s => s.FkSchoolClassesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requests == null)
            {
                return NotFound();
            }

            return View(requests);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requests = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(requests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestsExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
