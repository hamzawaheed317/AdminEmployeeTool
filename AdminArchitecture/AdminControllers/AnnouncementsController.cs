using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminEmployeeTool.Data;
using AdminEmployeeTool.Models;

namespace AdminEmployeeTool.AdminArchitecture.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Announcements
        public async Task<IActionResult> Index()
        {
            if (_context.Announcement == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Announcement' is null.");
            }

            var announcements = await _context.Announcement
                .OrderByDescending(a => a.IsPinned) // Pinned announcements first
                .ThenByDescending(a => a.PostedDate) // Optionally, order by date within pinned announcements
                .ToListAsync();

            return View(announcements);
        }


        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // GET: Announcements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Author,IsPinned,ImageUrl,VideoUrl")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.PostedDate = DateTime.Now;
                announcement.LikesCount = 0;

                _context.Add(announcement);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(announcement);
        }

        [HttpPost]
        public IActionResult UploadImage(int announcementId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var uniqueFileName = $"{announcementId}_{Guid.NewGuid().ToString()}_{file.FileName}";

                // Ensure the folder exists
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Return the file name as JSON response
                return Json(new { fileName = uniqueFileName });
            }

            // If file is null or empty, return an error response
            return BadRequest("File upload failed");
        }
        [HttpPost]
        public IActionResult UploadVideo(int announcementId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos");
                var uniqueFileName = $"{announcementId}_{Guid.NewGuid().ToString()}_{file.FileName}";

                // Ensure the folder exists
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Return the file name as JSON response
                return Json(new { fileName = uniqueFileName });
            }

            // If file is null or empty, return an error response
            return BadRequest("File upload failed");
        }


        // GET: Announcements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Author,PostedDate,IsPinned,LikesCount,ImageUrl,VideoUrl")] Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementExists(announcement.Id))
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
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announcement == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Announcement'  is null.");
            }
            var announcement = await _context.Announcement.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcement.Remove(announcement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementExists(int id)
        {
            return (_context.Announcement?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
