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

        public StartController(BooksUseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(BooksUseContext context)
        {
            //get current user
            var currentUser = await _context.Users.FirstOrDefaultAsync(r => r.IntranetUserId == config.intranetId);

            if(currentUser.FkRoles == 1)
            {
                return RedirectToAction("Index", "Requests");
            }
            else if(currentUser.FkRoles == 2)
            {
                return RedirectToAction("Index", "Books");
            }
            else
            {
                return NotFound();
            }

            
        }
    }
}