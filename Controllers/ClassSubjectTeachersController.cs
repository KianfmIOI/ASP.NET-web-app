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
    public class ClassSubjectTeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassSubjectTeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassSubjectTeachers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassSubjectTeachers
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .Include(c => c.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassSubjectTeachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSubjectTeacher = await _context.ClassSubjectTeachers
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .Include(c => c.Teacher)
                .OrderBy(c=>c.ClassId)
                .ThenBy(s=>s.SubjectId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classSubjectTeacher == null)
            {
                return NotFound();
            }

            return View(classSubjectTeacher);
        }

        // GET: ClassSubjectTeachers/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName");
            return View();
        }

        // POST: ClassSubjectTeachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassId,SubjectId,TeacherId")] ClassSubjectTeacher classSubjectTeacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classSubjectTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classSubjectTeacher.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName", classSubjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", classSubjectTeacher.TeacherId);
            return View(classSubjectTeacher);
        }

        // GET: ClassSubjectTeachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSubjectTeacher = await _context.ClassSubjectTeachers.FindAsync(id);
            if (classSubjectTeacher == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classSubjectTeacher.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName", classSubjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", classSubjectTeacher.TeacherId);
            return View(classSubjectTeacher);
        }

        // POST: ClassSubjectTeachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassId,SubjectId,TeacherId")] ClassSubjectTeacher classSubjectTeacher)
        {
            if (id != classSubjectTeacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSubjectTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassSubjectTeacherExists(classSubjectTeacher.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", classSubjectTeacher.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName", classSubjectTeacher.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", classSubjectTeacher.TeacherId);
            return View(classSubjectTeacher);
        }

        // GET: ClassSubjectTeachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSubjectTeacher = await _context.ClassSubjectTeachers
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classSubjectTeacher == null)
            {
                return NotFound();
            }

            return View(classSubjectTeacher);
        }

        // POST: ClassSubjectTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classSubjectTeacher = await _context.ClassSubjectTeachers.FindAsync(id);
            if (classSubjectTeacher != null)
            {
                _context.ClassSubjectTeachers.Remove(classSubjectTeacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassSubjectTeacherExists(int id)
        {
            return _context.ClassSubjectTeachers.Any(e => e.Id == id);
        }
    }
}
