using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vedma0.Data;
using Vedma0.Models.Helper;
using Microsoft.EntityFrameworkCore;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class GameLobbyController : VedmaController
    {
        public GameLobbyController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: GameLobby
        public async Task<ActionResult> Index()
        {
            var game = await _context.Games.Include(g=>g.GameUsers).ThenInclude(gu=>gu.VedmaUser).FirstOrDefaultAsync(g => g.Id == (Guid)GameId());
            var users = game.GameUsers.Select(gu => gu.VedmaUser);
            ViewBag.game = await GameAsync();
            return View(users);
        }

        // GET: GameLobby/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GameLobby/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameLobby/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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