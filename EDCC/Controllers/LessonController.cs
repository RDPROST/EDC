using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDCC.Data;
using EDCC.Models;
using File = EDCC.Models.File;

namespace EDCC.Controllers
{
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _hostingEnvironment;

        public LessonController(ApplicationDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: Lesson
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lessons.Include(l => l.File).Include(l => l.Group).Include(l => l.Subject).Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lesson/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.File)
                .Include(l => l.Group)
                .Include(l => l.Subject)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lesson/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName");
            return View();
        }

        // POST: Lesson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeSlot,Date,GroupId,UserId,SubjectId,FileId")] Lesson lesson, IFormFile file)
        {
            
            if (!ModelState.IsValid)
            {
                if (file != null)
                {
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    if (file.Length > 0) {
                        string filePath = Path.Combine(uploads, file.FileName);
                        using (Stream fileStream = new FileStream(@filePath, FileMode.Create)) {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    lesson.File = new File()
                    {
                        Name = file.FileName
                    };
                
                    lesson.FileId = lesson.File.Id;
                }
                
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lesson.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", lesson.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", lesson.UserId);
            return View(lesson);
        }

        // GET: Lesson/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lesson.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", lesson.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", lesson.UserId);
            return View(lesson);
        }

        // POST: Lesson/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeSlot,Date,GroupId,UserId,SubjectId,FileId")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lesson.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", lesson.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "UserName", lesson.UserId);
            return View(lesson);
        }

        // GET: Lesson/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.File)
                .Include(l => l.Group)
                .Include(l => l.Subject)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lessons'  is null.");
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
          return (_context.Lessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
