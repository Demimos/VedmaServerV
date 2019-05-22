using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models;
using Vedma0.Models.GameEntities;
using Vedma0.Models.Helper;

namespace Vedma0.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<VedmaUser> _userManager;
        public CharactersController(UserManager<VedmaUser> manager, ApplicationDbContext contex)
        {
            _userManager = manager;
            _context = contex;

        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("in_Game"))
                return Redirect("~/");
            if (!Guid.TryParse(HttpContext.Request.Cookies["in_Game"], out Guid Gid))
                return Redirect("~/");
            var game = await _context.Games.AsNoTracking().Include(g => g.GameUsers).FirstOrDefaultAsync();
            if (AccessHandle.GameMasterCheck(HttpContext, await _userManager.GetUserAsync(HttpContext.User), game))
                return View("AccessDenied");
            var applicationDbContext = _context.Characters.AsNoTracking().Include(c => c.User).Where(c=>c.GameId==Gid);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Game)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Active,HasSuspendedSignal,InActiveMessage,Id,Name,GameId")] Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", character.GameId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", character.UserId);
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", character.GameId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", character.UserId);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserId,Active,HasSuspendedSignal,InActiveMessage,Id,Name,GameId")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", character.GameId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", character.UserId);
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Game)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(long id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
