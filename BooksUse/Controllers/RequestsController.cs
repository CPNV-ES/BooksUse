using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksUse.Controllers;
using Microsoft.AspNetCore.Http;
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

        // Before loading any page
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Update current year
            StartController._currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);

            // Set user in viewbag
            ViewBag.user = StartController._currentUser;
            base.OnActionExecuting(filterContext);
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            // update year
            StartController._currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);

            var booksUseContext = _context.Requests
                .Include(r => r.FkBooksNavigation)
                .Include(r => r.FkUsersNavigation)
                .Include(r => r.FkYearsNavigation)
                .Include(r => r.SchoolClassesRequests)
                .ThenInclude( s => s.FkSchoolClassesNavigation)
                .Where(r => r == r);

            // if a year is open get requests of the year
            if (StartController._currentYear != null)
            {
                booksUseContext = booksUseContext.Where(r => r.FkYears == StartController._currentYear.Id);
            }

            // if it's a teacher, only show personnal request order by approved
            if (StartController._currentUser.FkRoles == 1)
            {
                booksUseContext = booksUseContext.Where(r => r.FkUsersNavigation.Id == StartController._currentUser.Id).OrderBy(r => r.Approved);
                return View("indexTeachers", await booksUseContext.ToListAsync());
            }
            // Set view name
            ViewData["Name"] = "Liste des demandes";

            return View(await booksUseContext.ToListAsync());
        }

        public async Task<IActionResult> IndexPending()
        {
            // update year
            StartController._currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);

            var booksUseContext = _context.Requests
                .Include(r => r.FkBooksNavigation)
                .Include(r => r.FkUsersNavigation)
                .Include(r => r.FkYearsNavigation)
                .Include(r => r.SchoolClassesRequests)
                .ThenInclude(s => s.FkSchoolClassesNavigation)
                .Where(r => r.FkYearsNavigation.Open == true);

            // if there is no year return all requests
            if (StartController._currentYear == null)
            {
                return View("Index", await booksUseContext.ToListAsync());
            }

            //Get requests of current year, who don't approved
            booksUseContext = booksUseContext.Where(r => r.FkYears == StartController._currentYear.Id && r.Approved == 0);

            //Set view name
            ViewData["Name"] = "Liste des demandes en attente d'approbation";

            return View("Index", await booksUseContext.ToListAsync());
        }

        // aproved a request
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
            // change status
            requests.Approved = 1;

            // Update
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
        public async Task<IActionResult> Create(int booksId = -1)
        {
            // Get schoolClasses from table
            ViewData["SchoolClasses"] = new SelectList(_context.SchoolClasses, "Id", "Name");

            // if it's teacher
            if (StartController._currentUser.FkRoles == 1)
            {
                // Get teacher requests
                var requests = await _context.Requests.Where(r => r.FkUsers == StartController._currentUser.Id).ToListAsync();

                // Get books id of current user requests
                var idBooks = requests.Select(b => b.FkBooks).ToArray();

                // Get avaible books for user
                var books = await _context.Books.Where(r => Array.IndexOf(idBooks, r.Id) == -1).ToListAsync();

                // If there is a default item
                if(booksId != -1)
                {
                    // if the function have default item set it
                    ViewData["FkBooks"] = new SelectList(books, "Id", "Title", booksId);
                }
                else
                {
                    ViewData["FkBooks"] = new SelectList(books, "Id", "Title");
                }


                // return createteachers view
                return View("createTeachers");
            }
            else
            {
                // Get all users append firstname and lastname
                ViewData["FkUsers"] = from u in _context.Users.Where(r => r.FkRoles == 1)
                                      select new SelectListItem
                                      {
                                          Value = u.Id.ToString(),
                                          Text = u.FirstName + " " + u.LastName
                                      };

                // Get all books
                ViewData["FkBooks"] = new SelectList(_context.Books, "Id", "Title");
                return View();
            }

            
        }


        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ForYear,Approved,FkUsers,FkBooks,SchoolClassesRequests")] Requests requests)
        {
            if (ModelState.IsValid)
            {
                // Set property of object (Years, Approved)
                requests.FkYears = StartController._currentYear.Id;
                requests.Approved = 1;

                // if there no user in request set it automaticlly
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
                
                // get the id of selected classes
                var schoolClassesIds = Request.Form["SchoolClassesRequests"];

                // For each id create a schoolclassesrequestd
                foreach (string schoolClassesId in schoolClassesIds)
                {
                    // create an object with parameters
                    SchoolClassesRequests schoolClassesRequest = new SchoolClassesRequests { FkRequests = requests.Id, FkSchoolClasses = Int32.Parse(schoolClassesId) };
                    _context.Add(schoolClassesRequest);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(requests);
            
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

            ViewData["SchoolClassesRequests"] = from u in _context.SchoolClassesRequests
                                  select new SelectListItem
                                  {
                                      Value = u.FkSchoolClassesNavigation.Id.ToString(),
                                      Text = u.FkSchoolClassesNavigation.Name
                                  };

            return View(requests);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ForYear,Approved,FkUsers,FkBooks,SchoolClassesRequests")] Requests requests)
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
            ViewData["SchoolClassesRequests"] = from u in _context.SchoolClassesRequests
                                                select new SelectListItem
                                                {
                                                    Value = u.FkSchoolClassesNavigation.Id.ToString(),
                                                    Text = u.FkSchoolClassesNavigation.Name
                                                };
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

            var scr = await _context.SchoolClassesRequests.Where(r => r.FkRequests == id).ToListAsync();
            foreach(SchoolClassesRequests el in scr)
            {
                _context.Remove(el);
            }
            
            await _context.SaveChangesAsync();

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
