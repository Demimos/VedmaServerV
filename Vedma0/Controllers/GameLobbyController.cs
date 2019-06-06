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

        // GET: GameLobby/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GameLobby/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameLobby/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GameLobby/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}