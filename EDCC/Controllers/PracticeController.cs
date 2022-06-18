using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDCC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EDCC.Controllers
{
    public class PracticeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracticeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Students"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }
    }
}