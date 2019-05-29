using System;
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
using Vedma0.Models.ManyToMany;
using Vedma0.Models.ViewModels;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class CharactersController : VedmaController
    {
        public CharactersController(ApplicationDbContext context):base(context)
        {
        }


        // GET: Characters

        public async Task<IActionResult> Index()
        {
            var gid = GameId();
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
            var game = await GameAsync();
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
            var userList = new List<Selectable>() { new Selectable(null, "") };
            userList.AddRange(_context.Users.Select(u => new Selectable(u.Id, u.UserName)));
            ViewData["UserId"] = new SelectList(userList, "Id", "Name", userList.First().Id);
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
                character.GameId = (Guid)GameId();
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userList = new List<Selectable>() { new Selectable { Id = null, Name = "" } };
            userList.AddRange(_context.Users.Select(u => new Selectable(u.Id, u.UserName)));
            ViewData["UserId"] = new SelectList(userList, "Id", "Name", character.UserId);
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gid = (Guid)GameId();
            var character = await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id && c.GameId==gid);
            if (character == null)
            {
                return NotFound();
            }
            var userList = new List<Selectable>() { new Selectable { Id=null, Name="" } };
            userList.AddRange(_context.Users.Select(u => new Selectable(u.Id, u.UserName)));
            ViewData["UserId"] = new SelectList(userList, "Id", "Name", character.UserId);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserId,Active,HasSuspendedSignal,InActiveMessage,Id,Name")] Character character)
        {
            var gid = (Guid)GameId();
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", character.UserId);
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Guid gid = (Guid)GameId();
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
            var gid = (Guid)GameId();
            var character = await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id && gid==c.GameId);
            if (character == null)
                return NotFound();
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Delete/Presets/5
        public async Task<IActionResult> Presets(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Guid gid = (Guid)GameId();
            var character = await _context.Characters.AsNoTracking().Include(m=>m.EntityPresets)
                .FirstOrDefaultAsync(m => m.Id == id && m.GameId == gid);
            if (character == null)
            {
                return NotFound();
            }
            var presets =await _context.Presets.AsNoTracking()
                .Where(p => p.GameId == gid).ToListAsync();
            foreach (var preset in presets)
            {
                var entityPreset = character.EntityPresets.FirstOrDefault(ep => ep.PresetId == preset.Id);
                if (entityPreset != null)
                    preset.EntityPresets.Add(entityPreset);
            }
           
            return View(presets);
        }

        [HttpPost, ActionName("Presets")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPresets(long id, [FromBody] IList<long> presetIds)
        {
            var gid = (Guid)GameId();
            var character = await _context.Characters.Include(c=>c.EntityPresets).FirstOrDefaultAsync(c => c.Id == id && gid == c.GameId);
            if (character == null)
                return NotFound();
            var newIds = presetIds.Except(character.EntityPresets.Select(ep => ep.PresetId)).ToList();
            var presets = await _context.Presets.Where(p => p.GameId==(Guid)GameId() && newIds.Contains(p.Id)).ToListAsync();
            foreach (var ep in character.EntityPresets.Where(e => !presetIds.Contains(e.PresetId)))
                character.EntityPresets.Remove(ep);
            ((List<EntityPreset>)character.EntityPresets).AddRange(presets.Select(p => new EntityPreset { GameEntityId = id, PresetId = p.Id }));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id });
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
