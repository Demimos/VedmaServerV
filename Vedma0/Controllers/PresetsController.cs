using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models;
using Vedma0.Models.Helper;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class PresetsController : VedmaController
    {

        public PresetsController(ApplicationDbContext context):base(context)
        {
        }
       
        // GET: Presets
        public async Task<IActionResult> Index()
        {
            var gid = (Guid)GameId();
            var applicationDbContext = _context.Presets.AsNoTracking().Where(p=>p.GameId==gid);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Presets/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets
                .Include(p => p.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preset == null)
            {
                return NotFound();
            }

            return View(preset);
        }

        // GET: Presets/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name");
            return View();
        }

        // POST: Presets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameId,SortValue,_Abilities,Name,Title,Description,SelfInsight")] Preset preset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", preset.GameId);
            return View(preset);
        }

        // GET: Presets/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets.FindAsync(id);
            if (preset == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", preset.GameId);
            return View(preset);
        }

        // POST: Presets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GameId,SortValue,_Abilities,Name,Title,Description,SelfInsight")] Preset preset)
        {
            if (id != preset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresetExists(preset.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", preset.GameId);
            return View(preset);
        }

        // GET: Presets/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets
                .Include(p => p.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preset == null)
            {
                return NotFound();
            }

            return View(preset);
        }

        // POST: Presets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var preset = await _context.Presets.FindAsync(id);
            _context.Presets.Remove(preset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresetExists(long id)
        {
            return _context.Presets.Any(e => e.Id == id);
        }
    }
}
