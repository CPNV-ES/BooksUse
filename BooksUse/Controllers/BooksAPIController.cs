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
            // Get reuquest of userID
            var requests = await _context.Requests.Where(r => r.FkUsers == userid).ToListAsync();

            // Get all books id of request
            var idBooks = requests.Select(b => b.FkBooks).ToArray();

            // Get all avaible books for user
            var books = await _context.Books.Where(r => Array.IndexOf(idBooks, r.Id) == -1).ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return books;
        }

        // GET: api/BooksAPI/Exist/field/value
        [HttpGet("Exist/{field}/{value}")]
        public async Task<ActionResult<List<Books>>> Exist(string field, string value)
        {
            Books book = null;
            switch(field)
            {
                case "title":
                    // Get first value if exist
                    book = await _context.Books.Where(r => r.Title == value).FirstOrDefaultAsync();
                    break;
                case "isbn":
                    // Get first value if exist
                    book = await _context.Books.Where(r => r.Isbn == value).FirstOrDefaultAsync();
                    break;
                default:
                    break;
            }

            if (book == null)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
