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

      

        // GET: Diary/Create
        public async Task<IActionResult> Create()
        {
            var game = await GameAsync();
            if (IsMaster(game))
            {
                ViewData["IsMaster"] = "true";
                ViewData["Id"] = game.Id;
            }
            ViewData["Title"] = game.Name;
            return View();
        }

        // POST: Diary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Message")] DiaryPage diaryPage)
        {
            if (ModelState.IsValid)
            {
                if (diaryPage.Title == null || diaryPage.Message == null)
                    return View();
                diaryPage.Type = DiaryPageType.User;
                diaryPage.GameId = (Guid)GameId();
                diaryPage.CharacterId = (await GetCharacter()).Id;
                diaryPage.DateTime = DateTime.UtcNow;
                _context.Diary.Add(diaryPage);
                await _context.SaveChangesAsync(); ;
                return RedirectToAction(nameof(Index), "InGame",null);
            }
            var game = await GameAsync();
            if (IsMaster(game))
            {
                ViewData["IsMaster"] = "true";
                ViewData["Id"] = game.Id;
            }
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
