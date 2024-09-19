using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminEmployeeTool.Data;
using AdminEmployeeTool.Models;

namespace AdminEmployeeTool.Controllers
{
    public class EventViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventViewModels
        public async Task<IActionResult> Index()
        {
              return _context.EventViewModel != null ? 
                          View(await _context.EventViewModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.EventViewModel'  is null.");
        }
        [HttpGet]
        public JsonResult GetEvents()
        {
            var events = _context.EventViewModel.Select(e => new
            {
                id = e.Id,
                title = e.Title,
                date = e.Date.ToString("yyyy-MM-dd"),
                startTime = e.StartTime.ToString(@"hh\:mm"),
                endTime = e.EndTime.ToString(@"hh\:mm"),
                description = e.Description,
                label = e.Label
            }).ToList();

            return Json(events);
        }
        // GET: EventViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventViewModel == null)
            {
                return NotFound();
            }

            var eventViewModel = await _context.EventViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventViewModel == null)
            {
                return NotFound();
            }

            return View(eventViewModel);
        }

        // GET: EventViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,StartTime,EndTime,Repeat,Location,Attendees,Description,Reminder,Label")] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventViewModel);
        }

        // GET: EventViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventViewModel == null)
            {
                return NotFound();
            }

            var eventViewModel = await _context.EventViewModel.FindAsync(id);
            if (eventViewModel == null)
            {
                return NotFound();
            }
            return View(eventViewModel);
        }

        // POST: EventViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,StartTime,EndTime,Repeat,Location,Attendees,Description,Reminder,Label")] EventViewModel eventViewModel)
        {
            if (id != eventViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventViewModelExists(eventViewModel.Id))
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
            return View(eventViewModel);
        }

        // GET: EventViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventViewModel == null)
            {
                return NotFound();
            }

            var eventViewModel = await _context.EventViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventViewModel == null)
            {
                return NotFound();
            }

            return View(eventViewModel);
        }

        // POST: EventViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EventViewModel'  is null.");
            }
            var eventViewModel = await _context.EventViewModel.FindAsync(id);
            if (eventViewModel != null)
            {
                _context.EventViewModel.Remove(eventViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventViewModelExists(int id)
        {
          return (_context.EventViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
