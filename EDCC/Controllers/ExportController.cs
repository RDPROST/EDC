using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDCC.Data;
using EDCC.Models;
using Fingers10.ExcelExport.ActionResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EDCC.Controllers
{
    public class ExportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int Id)
        {
            List<Student> students = await _context.Students.Where(x => x.GroupId == Id).ToListAsync();
            List<Lesson> lessons = await _context.Lessons.Where(l => l.GroupId == Id).ToListAsync();

            return new ExcelResult<Student>(students, "Sheet1", "StudentsReport");
        }
    }
}