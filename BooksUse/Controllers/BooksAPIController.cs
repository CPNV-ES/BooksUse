using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksUse.Models;

namespace BooksUse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksAPIController : ControllerBase
    {
        private readonly BooksUseContext _context;

        public BooksAPIController(BooksUseContext context)
        {
            _context = context;
        }

        // GET: api/BooksAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/BooksAPI/GetAvaibleBooks/1
        [HttpGet("GetAvaibleBooks/{userid}")]
        public async Task<ActionResult<List<Books>>> GetAvaibleBooks(int userid)
        {
            var requests = await _context.Requests.Where(r => r.FkUsers == userid).ToListAsync();
            var idBooks = requests.Select(b => b.FkBooks).ToArray();

            var books = await _context.Books.Where(r => Array.IndexOf(idBooks, r.Id) == -1).ToListAsync();



            if (books == null)
            {
                return NotFound();
            }

            return books;
        }

        // PUT: api/BooksAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooks(int id, Books books)
        {
            if (id != books.Id)
            {
                return BadRequest();
            }

            _context.Entry(books).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BooksAPI
        [HttpPost]
        public async Task<ActionResult<Books>> PostBooks(Books books)
        {
            _context.Books.Add(books);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooks", new { id = books.Id }, books);
        }

        // DELETE: api/BooksAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Books>> DeleteBooks(int id)
        {
            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }

            _context.Books.Remove(books);
            await _context.SaveChangesAsync();

            return books;
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
