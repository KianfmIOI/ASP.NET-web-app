using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp.Data;
using SchoolManagementWebApp.Models.SchoolManagementWebApp.Models;

namespace SchoolManagementWebApp.Controllers
{
    public class GradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Grades.Include(g => g.Class).Include(g => g.Student).Include(g => g.Subject).Include(g => g.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Class)
                .ThenInclude(g => g.Students)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,TeacherId,SubjectId,ClassId,comment,Score,DateAssigned")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", grade.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", grade.TeacherId);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", grade.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", grade.TeacherId);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,TeacherId,SubjectId,ClassId,comment,Score,DateAssigned")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", grade.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "SubjectName", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullName", grade.TeacherId);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Class)
                .ThenInclude(g => g.Students)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
