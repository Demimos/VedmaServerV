using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models.Helper;
using Vedma0.Models.Properties;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class BasePropertiesController : VedmaController
    {

        public BasePropertiesController(ApplicationDbContext context):base(context)
        {
        }

        // GET: BaseProperties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BaseProperties.Include(b => b.Game).Include(b => b.Preset);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BaseProperties/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var baseProperty = await _context.BaseProperties
        //        .Include(b => b.Game)
        //        .Include(b => b.Preset)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (baseProperty == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(baseProperty);
        //}

        // GET: BaseProperties/Create
        public IActionResult CreateText( long? Id)
        {
            if (Id==null||!PresetExists((long)Id))
            {
                return BadRequest();
            }
            ViewBag.PresetId = Id;
            return View();
        }

        // POST: BaseProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateText( [Bind("Name,Description,PresetId,Visible,DefaultValue")] BaseTextProperty baseProperty)
        {
            if (ModelState.IsValid && PresetExists((long)baseProperty.PresetId))
            {
                baseProperty.GameId = (Guid)GameId();
                _context.Add(baseProperty);
                await _context.SaveChangesAsync();
                baseProperty.SortValue = baseProperty.Id;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baseProperty);
        }

        // GET: BaseProperties/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseProperty = await _context.BaseProperties.FindAsync(id);
            if (baseProperty == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", baseProperty.GameId);
            ViewData["PresetId"] = new SelectList(_context.Presets, "Id", "Name", baseProperty.PresetId);
            return View(baseProperty);
        }

        // POST: BaseProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,GameId,Description,PresetId,SortValue,Visible")] BaseProperty baseProperty)
        {
            if (id != baseProperty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baseProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasePropertyExists(baseProperty.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", baseProperty.GameId);
            ViewData["PresetId"] = new SelectList(_context.Presets, "Id", "Name", baseProperty.PresetId);
            return View(baseProperty);
        }

        // GET: BaseProperties/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseProperty = await _context.BaseProperties
                .Include(b => b.Game)
                .Include(b => b.Preset)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baseProperty == null)
            {
                return NotFound();
            }

            return View(baseProperty);
        }

        // POST: BaseProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var baseProperty = await _context.BaseProperties.FindAsync(id);
            _context.BaseProperties.Remove(baseProperty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasePropertyExists(long id)
        {
            return _context.BaseProperties.Any(e => e.Id == id);
        }
        private bool PresetExists(long id)
        {
            return _context.Presets.Any(e => e.Id == id && e.GameId==GameId());
        }
    }
}
