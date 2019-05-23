﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [AccessRule(AccessLevel.Developer)]
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Id игры
        /// </summary>
        private Guid GetGid()
        {
            var GameId = Request.Cookies["in_game"];
            return Guid.Parse(GameId);
        }
        /// <summary>
        /// Возвращает текущую игру
        /// </summary>
        private async Task<Game> GetGameAsync()
        {
            return await _context.Games.FindAsync(GetGid());
        }
        /// <summary>
        /// Id пользователя
        /// </summary>
        private string GetUid()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        // GET: Characters

        public async Task<IActionResult> Index()
        {
            var gid = GetGid();
            var applicationDbContext = _context.Characters.AsNoTracking().Include(c => c.User).Where(c=>c.GameId==gid);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var game = await GetGameAsync();
            var character = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id && game.Id==m.GameId);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Active,HasSuspendedSignal,InActiveMessage,Name,UserId")] Character character)
        {
            if (ModelState.IsValid)
            {
                character.GameId = GetGid(); ;
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            var gid = GetGid();
            var character = await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id && c.GameId==gid);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", character.UserId);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserId,Active,HasSuspendedSignal,InActiveMessage,Id,Name")] Character character)
        {
            var gid = GetGid();
            if (id != character.Id || !CharacterExists(id, gid))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    character.GameId = gid;
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
            var gid = GetGid();
            var character = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id && m.GameId==gid);
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
            var gid = GetGid();
            var character = await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id && gid==c.GameId);
            if (character == null)
                return NotFound();
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(long id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
        private bool CharacterExists(long id, Guid gid)
        {
            return _context.Characters.Any(e => e.Id == id && e.GameId==gid);
        }
    }
}
