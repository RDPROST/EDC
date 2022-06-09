using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EDCC.Data;
using EDCC.Fill;
using EDCC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDCC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ScheduleController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public static DateTime GetFirstDateOfWeek(int weekNumber)
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, 1, 1); //1 января сего года
            while (firstDay.DayOfWeek != DayOfWeek.Monday) firstDay = firstDay.AddDays(-1); //ближайший предыдущий понедельник
            return firstDay.AddDays(7 * weekNumber); //добавляем количество недель * 7 дней
        }
        
        private static int arrSortDate(Lesson lesson, Lesson lesson1)
        {
            if (lesson.Date == null && lesson1.Date == null)
            {
                return 0;
            }
            else if (lesson.Date > lesson1.Date)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        
        private static int arrSortTimeSlot(Lesson lesson, Lesson lesson1)
        {
            if (lesson.TimeSlot == null && lesson1.TimeSlot == null)
            {
                return 0;
            }
            else if (lesson.TimeSlot > lesson1.TimeSlot)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cal = new GregorianCalendar();
            var weekNumber = cal.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            var firstDay = GetFirstDateOfWeek(weekNumber);
            var lastDay = GetFirstDateOfWeek(weekNumber).AddDays(6);
            var group = await _context.Students
                .Where(u => u.UserId == user.Id)
                .ToListAsync();
            if (group.Count <= 0)
            {
                return NotFound();
            }
            var arrRes = await _context.Lessons
                .Where(g => g.GroupId == group[0].GroupId)
                .Include(s=> s.Subject)
                .Include(l => l.User)
                .Include(f=> f.File)
                .ToListAsync();
            List<ScheduleFill> applicationDbContext = new List<ScheduleFill>();

            for (int i = 0; i < 7; i++)
            {
                List<Lesson> objLessons = new List<Lesson>();
                foreach (var item in arrRes)
                {
                    if (item.Date == firstDay.AddDays(i))
                    {
                        objLessons.Add(item);
                    }
                }
                objLessons.Sort(arrSortTimeSlot);
                objLessons.Sort(arrSortDate);
                var obj = new ScheduleFill()
                {
                    Name = firstDay.AddDays(i).DayOfWeek.ToString(),
                    Lessons = objLessons
                };
                applicationDbContext.Add(obj);
            }

            return View(applicationDbContext);
        }

        public async Task<IActionResult> Lesson(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(s=> s.Subject)
                .Include(l => l.User)
                .Include(g => g.Group)
                .Include(f => f.File)
                .FirstOrDefaultAsync(l => l.Id == id);

            var mark = await _context.Marks?.Where(o => o.LessonId == lesson.Id).ToListAsync();
            if (mark.Count != 0)
            {
                ViewData["Mark"] = mark[0].MarkStudent.ToString();
            }
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }
    }
}