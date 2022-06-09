using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDCC.Data;
using EDCC.Models;

namespace EDCC.Controllers
{
    public class MarkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarkController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mark
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Marks.Include(m => m.Lesson).Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mark/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .Include(m => m.Lesson)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // GET: Mark/Create
        public IActionResult Create()
        {
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "UserId");
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Mark/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MarkStudent,LessonId,UserId")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "UserId", mark.LessonId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mark.UserId);
            return View(mark);
        }

        // GET: Mark/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks.FindAsync(id);
            if (mark == null)
            {
                return NotFound();
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "UserId", mark.LessonId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mark.UserId);
            return View(mark);
        }

        // POST: Mark/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MarkStudent,LessonId,UserId")] Mark mark)
        {
            if (id != mark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkExists(mark.Id))
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
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "UserId", mark.LessonId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mark.UserId);
            return View(mark);
        }

        // GET: Mark/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marks == null)
            {
                return NotFound();
            }

            var mark = await _context.Marks
                .Include(m => m.Lesson)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // POST: Mark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Marks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Marks'  is null.");
            }
            var mark = await _context.Marks.FindAsync(id);
            if (mark != null)
            {
                _context.Marks.Remove(mark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkExists(int id)
        {
          return (_context.Marks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
