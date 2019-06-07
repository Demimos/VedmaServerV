using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vedma0.Data;
using Vedma0.Models.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Vedma0.Models;
using Vedma0.Models.ManyToMany;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class GameLobbyController : VedmaController
    {
        private UserManager<VedmaUser> _userManager { get; set; }
        public GameLobbyController(UserManager<VedmaUser> userManager, ApplicationDbContext context) : base(context)
        {
            _userManager = userManager;
        }

        // GET: GameLobby
        public async Task<ActionResult> Index()
        {
            var game = await _context.Games.Include(g=>g.GameUsers).ThenInclude(gu=>gu.VedmaUser).FirstOrDefaultAsync(g => g.Id == (Guid)GameId());
            var users = game.GameUsers.Select(gu => gu.VedmaUser);
            ViewBag.game = await GameAsync();
            return View(users);
        }



        // POST: GameLobby/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(string email)
        {
            if (email == null)
                return BadRequest();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            var game = await _context.Games.Include(g=>g.GameUsers).FirstOrDefaultAsync(g=>g.Id==(Guid)GameId());
            if (!game.GameUsers.Select(gu=>gu.VedmaUserId).Contains(user.Id))
            {
                game.GameUsers.Add(new GameUser {
                    GameId = game.Id,
                    VedmaUserId = user.Id
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: GameLobby/Bind/fvref4r34?characterid=1
        public async Task<ActionResult> Bind(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            ViewBag.userId = user.Id;
            var characters = await _context.Characters
                .AsNoTracking()
                .Where(c => c.GameId == (Guid)GameId() && c.UserId != null)
                .ToListAsync();
            return View(characters);
        }

        // POST: GameLobby/Bind/5fewef344c
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Bind(string id, long characterId)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var character =await _context.Characters.FindAsync(characterId);
            var gameId = (Guid)GameId();
            var game = await _context.Games
                .AsNoTracking()
                .Include(g => g.GameUsers)
                .FirstOrDefaultAsync(g => g.Id == gameId);
            if (character==null 
                || character.GameId!=gameId 
                || character.UserId!=null
                || !game.GameUsers.Select(gu=>gu.VedmaUserId).Contains(user.Id))
            {
                return NotFound();
            }
            try
            {
                character.UserId = user.Id;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.userId = user.Id;
                ViewBag.Error = "Произошла ошибка, попробуйте ещё раз";
                var characters = await _context.Characters
                    .AsNoTracking()
                    .Where(c => c.GameId == (Guid)GameId() && c.UserId != null)
                    .ToListAsync();
                return View(characters);
            }
        }

        // GET: GameLobby/Remove/fdsfsadcds5
        public async Task<ActionResult> Remove(string id)
        {
            if (id == null)
                return BadRequest();
            var game = await _context.Games
                .AsNoTracking()
                .Include(g => g.GameUsers)
                .FirstOrDefaultAsync(g => g.Id == (Guid)GameId());
            if (!game.GameUsers.Select(gu=>gu.VedmaUserId).Contains(id))
                return RedirectToAction(nameof(Index));
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                game.GameUsers.Remove(game.GameUsers.First(gu => gu.VedmaUserId == id));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // POST: GameLobby/Remove/5213r2d32crrr3f
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveConfirmed(string id)
        {
            if (id == null)
                return BadRequest();
            var game = await _context.Games
                .AsNoTracking()
                .Include(g => g.GameUsers)
                .FirstOrDefaultAsync(g => g.Id == (Guid)GameId());
            try
            {
                game.GameUsers.Remove(game.GameUsers.First(gu => gu.VedmaUserId == id));
                game.BlackList.Add(id);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}