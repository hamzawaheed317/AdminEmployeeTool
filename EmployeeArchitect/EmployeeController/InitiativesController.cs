using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminEmployeeTool.Data;
using AdminEmployeeTool.EmployeeArchitect.EmployeeModels;

namespace AdminEmployeeTool.EmployeeArchitect.EmployeeController
{
    public class InitiativesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InitiativesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Initiatives
        public async Task<IActionResult> Index()
        {
            return _context.Initiative != null ?
                        View(await _context.Initiative.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Initiative'  is null.");
        }

        // GET: Initiatives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Initiative == null)
            {
                return NotFound();
            }

            var initiative = await _context.Initiative
                .FirstOrDefaultAsync(m => m.id == id);
            if (initiative == null)
            {
                return NotFound();
            }

            return View(initiative);
        }

        // GET: Initiatives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Initiatives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,project_manager,objective,description,benefits,files_paths")] Initiative initiative)
        {
            if (ModelState.IsValid)
            {
                _context.Add(initiative);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(initiative);
        }

        // GET: Initiatives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Initiative == null)
            {
                return NotFound();
            }

            var initiative = await _context.Initiative.FindAsync(id);
            if (initiative == null)
            {
                return NotFound();
            }
            return View(initiative);
        }

        // POST: Initiatives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,project_manager,objective,description,benefits,files_paths")] Initiative initiative)
        {
            if (id != initiative.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(initiative);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InitiativeExists(initiative.id))
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
            return View(initiative);
        }

        // GET: Initiatives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Initiative == null)
            {
                return NotFound();
            }

            var initiative = await _context.Initiative
                .FirstOrDefaultAsync(m => m.id == id);
            if (initiative == null)
            {
                return NotFound();
            }

            return View(initiative);
        }

        // POST: Initiatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Initiative == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Initiative'  is null.");
            }
            var initiative = await _context.Initiative.FindAsync(id);
            if (initiative != null)
            {
                _context.Initiative.Remove(initiative);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InitiativeExists(int id)
        {
            return (_context.Initiative?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
