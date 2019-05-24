using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models;
using Vedma0.Models.Helper;
using Vedma0.Models.Properties;
using Vedma0.Models.ViewModels;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Player)]
    public class InGameController : VedmaController
    {
        public InGameController(ApplicationDbContext context):base(context)
        {  
        }
        // GET: InGame
        public async Task<ActionResult> Index(string Id)
        {
            var uid = UserId();
            var game = await GameAsync();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.UserId == uid && c.GameId== game.Id);
            if (IsMaster(game))
            {
                ViewData["IsMaster"] = "true";
                ViewData["Id"] = game.Id;
            }
            ViewData["Title"] = game.Name;
            if (character == null)
            {
                return View("NoCharacter");
            }
            character.Properties = await _context.Properties.Include(p=>p.Preset).Where(p => p.GameEntityId == character.Id &&  p.Visible).ToListAsync();
            return View(new CharacterMainView(character));
        }

        // GET: InGame/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InGame/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InGame/Create
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

        // GET: InGame/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InGame/Edit/5
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

        // GET: InGame/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InGame/Delete/5
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