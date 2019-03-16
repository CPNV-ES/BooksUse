﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BooksUse.Models
{
    public class RequestsController : Controller
    {
        private readonly BooksUseContext _context;
        private static Users _currentUser;

        public RequestsController(BooksUseContext context)
        {
            _context = context;
        }


        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_currentUser == null)
            {
                _currentUser = await _context.Users.FirstOrDefaultAsync(r => r.IntranetUserId == config.intranetId);
            }
            ViewBag.user = _currentUser; //Add whatever
            base.OnActionExecuting(filterContext);
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            //Get context
            var booksUseContext = _context.Requests.Include(r => r.FkBooksNavigation).Include(r => r.FkUsersNavigation).Where(r => r == r);

            // Check role
            if (_currentUser.FkRoles == 1)
            {
                booksUseContext = booksUseContext.Where(r => r.FkUsersNavigation.Id == _currentUser.Id);
                return View("indexTeachers", await booksUseContext.ToListAsync());
            }

            
            return View(await booksUseContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requests == null)
            {
                return NotFound();
            }

            return View(requests);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewData["FkBooks"] = new SelectList(_context.Books, "Id", "Title");
            if(_currentUser.FkRoles == 1)
            {
                return View("createTeachers");
            }
            else
            {
                return View();
            }

            
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForYear", "FkBooks")] Requests requests)
        {
            if(requests.ForYear != 0)
            {
                var yearRequests = _context.Requests.Include(r => r.FkBooksNavigation).Include(r => r.FkUsersNavigation).Where(r => r.ForYear == requests.ForYear - 1);
                foreach (var el in yearRequests)
                {
                    var newEl = new Requests { Approved = 0, FkBooks = el.FkBooks, FkUsers = el.FkUsers, ForYear = requests.ForYear };
                    _context.Add(newEl);
                }
                
            }
            else if (requests.FkBooks != 0)
            {
                var recentRequest = await _context.Requests.Include(r => r.FkBooksNavigation).Include(r => r.FkUsersNavigation).OrderByDescending(r => r.ForYear).FirstAsync();
                requests.ForYear = recentRequest.ForYear;

                requests.FkUsers = _currentUser.Id;

                requests.Approved = 1;

                _context.Add(requests);
            }

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
