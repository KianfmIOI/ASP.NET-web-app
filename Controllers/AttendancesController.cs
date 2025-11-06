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
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attendances
                .Include(a => a.ClassSubjectTeacher)
                .ThenInclude(c => c.Class)
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(t => t.Teacher)
                .Include(s => s.ClassSubjectTeacher)
                .ThenInclude(s => s.Subject)
                .Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.ClassSubjectTeacher)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FatherName");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,ClassSubjectTeacherId,Date,IsPresent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassSubjectTeacherId"] = new SelectList(_context.ClassSubjectTeachers, "Id", "Id", attendance.ClassSubjectTeacherId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FatherName", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
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

            ViewData["ClassSubjectTeacherId"] = new SelectList(classSubjectTeachersList, "Id", "DisplayName");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FatherName", attendance.StudentId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ClassSubjectTeacherId,Date,IsPresent")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Id))
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
            ViewData["ClassSubjectTeacherId"] = new SelectList(_context.ClassSubjectTeachers, "Id", "Id", attendance.ClassSubjectTeacherId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FatherName", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.ClassSubjectTeacher)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }
    }
}
