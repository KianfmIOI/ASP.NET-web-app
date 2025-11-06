using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Data;
using SchoolManagementWebApp.Models;

namespace SchoolManagementWebApp.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Schedules
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(c=>c.Class)
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(t=>t.Teacher)
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(s=>s.Subject)
                .Include(s => s.TimeSlot);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(c => c.Class)
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(t => t.Teacher)
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            var classSubjectTeachersList = _context.ClassSubjectTeachers
                    .Include(c => c.Class)
                    .Include(c => c.Subject)
                    .Include(c => c.Teacher)
                    .Select(c => new
                    {
                        Id = c.Id,
                        DisplayName = c.Class.Name + " - " + c.Subject.SubjectName + " - " + c.Teacher.FullName
                    }).ToList();

            ViewData["ClassSubjectTeacherId"] = new SelectList(classSubjectTeachersList, "Id", "DisplayName"); 
            ViewData["DayOfWeek"] = new SelectList(Enum.GetValues(typeof(DayOfWeek)));
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots, "Id", "Id");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DayOfWeek,TimeSlotId,ClassSubjectTeacherId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var classSubjectTeachersList = _context.ClassSubjectTeachers
                    .Include(c => c.Class)
                    .Include(c => c.Subject)
                    .Include(c => c.Teacher)
                    .Select(c => new
                    {
                        Id = c.Id,
                        DisplayName = c.Class.Name + " - " + c.Subject.SubjectName + " - " + c.Teacher.FullName
                    }).ToList();

            ViewData["ClassSubjectTeacherId"] = new SelectList(classSubjectTeachersList, "Id", "DisplayName", schedule.ClassSubjectTeacherId);
            ViewData["DayOfWeek"] = new SelectList(Enum.GetValues(typeof(DayOfWeek)),schedule.DayOfWeek);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots, "Id", "Id", schedule.TimeSlotId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            var classSubjectTeachersList = _context.ClassSubjectTeachers
    .Include(c => c.Class)
    .Include(c => c.Subject)
    .Include(c => c.Teacher)
    .Select(c => new
    {
        Id = c.Id,
        DisplayName = c.Class.Name + " - " + c.Subject.SubjectName + " - " + c.Teacher.FullName
    }).ToList();

            ViewData["ClassSubjectTeacherId"] = new SelectList(classSubjectTeachersList, "Id", "DisplayName", schedule.ClassSubjectTeacherId);
            ViewData["DayOfWeek"] = new SelectList(Enum.GetValues(typeof(DayOfWeek)),schedule.DayOfWeek);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots, "Id", "Id", schedule.TimeSlotId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DayOfWeek,TimeSlotId,ClassSubjectTeacherId")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            var classSubjectTeachersList = _context.ClassSubjectTeachers
        .Include(c => c.Class)
        .Include(c => c.Subject)
        .Include(c => c.Teacher)
        .Select(c => new
        {
            Id = c.Id,
            DisplayName = c.Class.Name + " - " + c.Subject.SubjectName + " - " + c.Teacher.FullName
        }).ToList();

            ViewData["ClassSubjectTeacherId"] = new SelectList(classSubjectTeachersList, "Id", "DisplayName", schedule.ClassSubjectTeacherId);
            ViewData["DayOfWeek"] = new SelectList(Enum.GetValues(typeof(DayOfWeek)),schedule.DayOfWeek);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots, "Id", "Id", schedule.TimeSlotId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.ClassSubjectTeacher)
                .Include(s => s.TimeSlot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
