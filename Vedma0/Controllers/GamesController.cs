using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models;

namespace Vedma0.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        UserManager<VedmaUser> _userManager;
        public GamesController(UserManager<VedmaUser> manager, ApplicationDbContext contex)
        {
            _userManager = manager;
            _context = contex;

        }
        private readonly ApplicationDbContext _context;

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null||!Guid.TryParse(id, out Guid gid))
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id ==gid);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,OwnerId,IncludeVR,IncludeGeo," +
            "IncludeGeoFence,IncludeNews,IncludeNewsPublishing,IncludeNewsRate,IncludeNewsComments," +
            "StartTime,EndTime,Active")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.OwnerId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || !Guid.TryParse(id, out Guid gid))
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(gid);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,OwnerId,IncludeVR,IncludeGeo,IncludeGeoFence," +
            "IncludeNews,IncludeNewsPublishing,IncludeNewsRate,IncludeNewsComments,StartTime,EndTime,Active")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || !Guid.TryParse(id, out Guid gid))
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == gid);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!Guid.TryParse(id, out Guid gid))
                return BadRequest();
            var game = await _context.Games.FindAsync(gid);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public RedirectToActionResult Enter(string Id)
        {
            if (Id == null||!Guid.TryParse(Id, out Guid gid))
            {
                return RedirectToAction("Index");
            }
            var cookies = HttpContext.Request.Cookies;
            if (cookies.ContainsKey("in_Game"))
                HttpContext.Response.Cookies.Delete("in_Game");
            HttpContext.Response.Cookies.Append("in_Game", gid.ToString());
            return RedirectToAction("Index", "InGame", null);
        }

        private bool GameExists(Guid id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

    }
}
