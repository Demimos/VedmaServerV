using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vedma0.Controllers;
using Vedma0.Data;
using Vedma0.Models.Helper;
using Vedma0.Models.Logging;


namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Player)]
    public class DiaryController : VedmaController
    {

        public DiaryController(ApplicationDbContext context):base(context)
        {
        }

        // GET: Diary
        public async Task<IActionResult> Index()
        {
            var character = await _context.Characters
                .AsNoTracking()
                .Include(c => c.Diary)
                .FirstOrDefaultAsync(d => d.GameId == (Guid)GameId() && d.UserId == UserId());
            return View(character.Diary);
        }

        // GET: Diary/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var diaryPage = await _context.Diary
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (diaryPage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(diaryPage);
        //}

        // GET: Diary/Create
        public IActionResult Create()
        {
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Discriminator");
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name");
            return View();
        }

        // POST: Diary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Message,DateTime,GameId,CharacterId,Type")] DiaryPage diaryPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diaryPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Discriminator", diaryPage.CharacterId);
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", diaryPage.GameId);
            return View(diaryPage);
        }

        // GET: Diary/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diaryPage = await _context.Diary.FindAsync(id);
            if (diaryPage == null)
            {
                return NotFound();
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Discriminator", diaryPage.CharacterId);
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", diaryPage.GameId);
            return View(diaryPage);
        }

        // POST: Diary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Message,DateTime,GameId,CharacterId,Type")] DiaryPage diaryPage)
        {
            if (id != diaryPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diaryPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiaryPageExists(diaryPage.Id))
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
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Discriminator", diaryPage.CharacterId);
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", diaryPage.GameId);
            return View(diaryPage);
        }

        // GET: Diary/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diaryPage = await _context.Diary
                .Include(d => d.Character)
                .Include(d => d.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diaryPage == null)
            {
                return NotFound();
            }

            return View(diaryPage);
        }

        // POST: Diary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var diaryPage = await _context.Diary.FindAsync(id);
            _context.Diary.Remove(diaryPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiaryPageExists(long id)
        {
            return _context.Diary.Any(e => e.Id == id);
        }
    }
}
