using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models.Helper;
using Vedma0.Models.ManyToMany;
using Vedma0.Models.ViewModels;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class ContactsController : VedmaController
    {
        public ContactsController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: Contacts
        public async Task<IActionResult> Index(long id)
        {
            var gid = GameId();
            var character = await _context.Characters
                .AsNoTracking()
                .Include(p=>p.Contacts)
                .ThenInclude(p=>p.Reflection)
                .FirstOrDefaultAsync(p => p.Id == id && p.GameId == (Guid)gid);
            character.Watchers= (await _context.Characters
                .AsNoTracking()
                .Include(p => p.Watchers)
                .ThenInclude(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id && p.GameId == (Guid)gid))
                .Watchers;
            return View(new ContactList(character));
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> EditContacts(long id)
        {
            var gid = GameId();
            var character = await _context.Characters
                .AsNoTracking()
                .Include(p => p.Contacts)
                .FirstOrDefaultAsync(p => p.Id == id && p.GameId == (Guid)gid);
            ViewBag.characters = await _context.Characters
                .AsNoTracking()
                .Where(p => p.GameId == gid)
                .Select(p => new { p.Name, p.Id })
                .ToListAsync();
            return View(character);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditContacts(long id, long[] ids)
        {
            var gid = GameId();
            var character = await _context.Characters
                .Include(p => p.Contacts)
                .FirstOrDefaultAsync(p => p.Id == id && p.GameId == (Guid)gid);
            if (character == null)
                return NotFound();
            var characters = await _context.Characters
                .Include(p=>p.Watchers)
                .Where(p => p.GameId == gid)
                .ToListAsync();
            var blackList = character.Contacts.Where(p => !ids.Contains((long)p.ReflectionId));
            foreach (var b in blackList)
                character.Contacts.Remove(b);
            var whiteList = characters.Where(p => ids.Contains(p.Id) && !character.Contacts.Select(c => c.ReflectionId).Contains(p.Id));
            foreach (var t in whiteList)
            {
                var contact = new CharacterReflection()
                {
                    Owner = character,
                    Reflection = t
                };
                character.Contacts.Add(contact);
                t.Watchers.Add(contact);
            }
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id });
            }
            catch
            {
                return View(character);
            }
        }

    }
}