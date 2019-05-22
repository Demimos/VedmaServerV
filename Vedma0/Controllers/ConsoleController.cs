using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vedma0.Data;
using Vedma0.Models;
using Vedma0.Models.Helper;

namespace Vedma0.Controllers
{
    [Authorize]
    public class ConsoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<VedmaUser> _userManager;
        public ConsoleController(UserManager<VedmaUser> manager, ApplicationDbContext contex)
        {
            _userManager = manager;
            _context = contex;

        }
        // GET: Console
        public async Task<ActionResult> Index(string Id)
        {
            if (Id==null)
                return NotFound();
            if (!Guid.TryParse(Id, out Guid Gid))
                return BadRequest();
            var game = await _context.Games.FindAsync(Gid);
            if (game == null)
                Response.Redirect("~/");
            if (!AccessHandle.GameMasterCheck(_context, await _userManager.GetUserAsync(HttpContext.User), game))
                return View("AccessDenied");
            return View(game);
        }

        // GET: Console/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Console/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Console/Create
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

        // GET: Console/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Console/Edit/5
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

        // GET: Console/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Console/Delete/5
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