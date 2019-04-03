using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksUse.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;

namespace BooksUse.Controllers
{
    public class SupplierSupplyBooksController : Controller
    {
        private readonly BooksUseContext _context;

        public SupplierSupplyBooksController(BooksUseContext context)
        {
            _context = context;
        }

        // Before loading any page
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Update current year
            StartController._currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);

            // Set user in viewbag
            ViewBag.user = StartController._currentUser; //Add whatever
            base.OnActionExecuting(filterContext);
        }

        // GET: SupplierSupplyBooks
        public async Task<IActionResult> Index(int? priority)
        {
            // include relation
            var booksUseContext = _context.SupplierSupplyBook.Include(s => s.Book).Include(s => s.Supplier);

            // create new list of SupplierSupplyBook
            List<SupplierSupplyBook> supplierSupplyBooks = new List<SupplierSupplyBook>();
            if (priority == 0)
            {
                // orderby Deldelay
                supplierSupplyBooks = await booksUseContext.OrderBy(r => r.Deldelay).ToListAsync();
            }
            else if (priority == 1)
            {
                // orderby Price
                supplierSupplyBooks = await booksUseContext.OrderBy(r => r.Price).ToListAsync();
            }


            // Create two list for supplierSupplyBookIds to send to view and books already checked
            List<int> supplierSupplyBookIds = new List<int>(), booksCheck = new List<int>();

            // Get the id of the element of the first supplier for a book
            foreach (var supplierSupplyBook in supplierSupplyBooks)
            {
                // If book never check
                if (booksCheck.IndexOf(supplierSupplyBook.BookId.Value) == -1)
                {
                    // Add for not check again
                    booksCheck.Add(supplierSupplyBook.BookId.Value);
                    // Add current id to list
                    supplierSupplyBookIds.Add(supplierSupplyBook.Id);
                }
            }

            // Get the element by the id previously selected
            var newBooksUseContext = await booksUseContext.Where(r => supplierSupplyBookIds.IndexOf(r.Id) != -1).ToListAsync();

            // Return view
            return View(newBooksUseContext);

            

        }

        // GET: SupplierSupplyBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierSupplyBook = await _context.SupplierSupplyBook
                .Include(s => s.Book)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierSupplyBook == null)
            {
                return NotFound();
            }

            return View(supplierSupplyBook);
        }

        // GET: SupplierSupplyBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: SupplierSupplyBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SupplierId,BookId,Price,Deldelay")] SupplierSupplyBook supplierSupplyBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplierSupplyBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", supplierSupplyBook.BookId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", supplierSupplyBook.SupplierId);
            return View(supplierSupplyBook);
        }

        // GET: SupplierSupplyBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierSupplyBook = await _context.SupplierSupplyBook.FindAsync(id);
            if (supplierSupplyBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", supplierSupplyBook.BookId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", supplierSupplyBook.SupplierId);
            return View(supplierSupplyBook);
        }

        // POST: SupplierSupplyBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SupplierId,BookId,Price,Deldelay")] SupplierSupplyBook supplierSupplyBook)
        {
            if (id != supplierSupplyBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierSupplyBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierSupplyBookExists(supplierSupplyBook.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Author", supplierSupplyBook.BookId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", supplierSupplyBook.SupplierId);
            return View(supplierSupplyBook);
        }

        // GET: SupplierSupplyBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierSupplyBook = await _context.SupplierSupplyBook
                .Include(s => s.Book)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierSupplyBook == null)
            {
                return NotFound();
            }

            return View(supplierSupplyBook);
        }

        // POST: SupplierSupplyBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplierSupplyBook = await _context.SupplierSupplyBook.FindAsync(id);
            _context.SupplierSupplyBook.Remove(supplierSupplyBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierSupplyBookExists(int id)
        {
            return _context.SupplierSupplyBook.Any(e => e.Id == id);
        }
    }
}
