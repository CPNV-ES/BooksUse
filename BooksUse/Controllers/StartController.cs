using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksUse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksUse.Controllers
{
    public class StartController : Controller
    {
        private readonly BooksUseContext _context;
        public static Users _currentUser;
        public static Years _currentYear;

        public StartController(BooksUseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(BooksUseContext context)
        {

            await setStaticVariables();

            if(_currentUser.FkRoles == 1)
            {
                return RedirectToAction("Index", "Requests");
            }
            else if(_currentUser.FkRoles == 2)
            {
                return RedirectToAction("IndexPending", "Requests");
            }
            else
            {
                return NotFound();
            }
 
        }

        public async Task setStaticVariables()
        {
            _currentUser = await _context.Users.FirstOrDefaultAsync(r => r.IntranetUserId == config.intranetId);
            _currentYear = await _context.Years.OrderByDescending(r => r.Title).FirstOrDefaultAsync(r => r.Open == true);
        }
    }
}