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
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendance/Manage?cstId=5
        public async Task<IActionResult> Manage(int cstId)
        {
            var cst = await _context.ClassSubjectTeachers
                .Include(x => x.Class).ThenInclude(c => c.Students)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == cstId);

            if (cst == null) return NotFound();

            ViewBag.Dates = Enumerable.Range(0, 14)
                .Select(offset => DateTime.Today.AddDays(-offset))
                .OrderBy(date => date)
                .ToList();

            var attendance = await _context.Attendances
                .Where(a => a.ClassId == cst.ClassId && a.SubjectId == cst.SubjectId)
                .ToListAsync();

            ViewBag.Attendance = attendance;

            return View(cst);
        }

        // POST: Toggle Attendance
        [HttpPost]
        public async Task<IActionResult> Toggle(int studentId, int classId, int subjectId, int teacherId, DateTime date)
        {
            var record = await _context.Attendances
                .FirstOrDefaultAsync(a => a.StudentId == studentId && a.ClassId == classId && a.SubjectId == subjectId && a.Date == date);

            if (record == null)
            {
                record = new Attendance
                {
                    StudentId = studentId,
                    ClassId = classId,
                    SubjectId = subjectId,
                    TeacherId = teacherId,
                    Date = date,
                    IsPresent = true
                };
                _context.Attendances.Add(record);
            }
            else
            {
                record.IsPresent = !record.IsPresent;
                _context.Attendances.Update(record);
            }

            await _context.SaveChangesAsync();
            return Ok(record.IsPresent);
        }
    }
}
