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
    public class StaffRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StaffRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.StaffRoles.ToListAsync());
        }

        // GET: StaffRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffRole = await _context.StaffRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffRole == null)
            {
                return NotFound();
            }

            return View(staffRole);
        }

        // GET: StaffRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleName")] StaffRole staffRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffRole);
        }

        // GET: StaffRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffRole = await _context.StaffRoles.FindAsync(id);
            if (staffRole == null)
            {
                return NotFound();
            }
            return View(staffRole);
        }

        // POST: StaffRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleName")] StaffRole staffRole)
        {
            if (id != staffRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffRoleExists(staffRole.Id))
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
            return View(staffRole);
        }

        // GET: StaffRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffRole = await _context.StaffRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffRole == null)
            {
                return NotFound();
            }

            return View(staffRole);
        }

        // POST: StaffRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffRole = await _context.StaffRoles.FindAsync(id);
            if (staffRole != null)
            {
                _context.StaffRoles.Remove(staffRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffRoleExists(int id)
        {
            return _context.StaffRoles.Any(e => e.Id == id);
        }
    }
}
