using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vedma0.Data;
using Vedma0.Models.GameEntities;
using Vedma0.Models.Helper;
using Vedma0.Models.Properties;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Developer)]
    public class PublishersController : VedmaController
    {
      

        public PublishersController(ApplicationDbContext context):base(context)
        {
         
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Publisher.AsNoTracking().Where(p=>p.GameId==GameId());
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher
                .AsNoTracking()
                .Include(m=>m.Articles)
                .FirstOrDefaultAsync(m => m.Id == id && m.GameId==GameId());
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
        
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Adress,Email,Image,AllowAnonymus,SortValue,Name,Description")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                publisher.SelfInsight = false;
                publisher.GameId = (Guid)GameId();
                publisher.Title = publisher.Name;
                _context.Add(publisher);
                await _context.SaveChangesAsync();

                BaseTextProperty pseudonym = new BaseTextProperty
                {
                    Name = $"Pseudonym for {publisher.Name}",
                    DefaultValue="Anonymus",
                    GameId=publisher.GameId,
                    PresetId=publisher.Id,
                    Description=$"Псевдоним для публикации и комментирование в журнале {publisher.Name}" 
                };
                BaseNumericProperty quotum = new BaseNumericProperty
                {
                    Name = $"Quota for publishing in {publisher.Name}",
                    DefaultValue = 0,
                    GameId = publisher.GameId,
                    PresetId = publisher.Id,
                    Description = $"Квота на публикации в журнале {publisher.Name}"
                };
                _context.BaseProperties.Add(pseudonym);
                _context.BaseProperties.Add(quotum);
                await _context.SaveChangesAsync();
                pseudonym.SortValue = pseudonym.Id;
                quotum.SortValue = quotum.Id;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher
                .AsNoTracking()
                .FirstOrDefaultAsync(p=>p.Id==id&&p.GameId==GameId());
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Adress,Email,Image,AllowAnonymus,Id,GameId,SortValue,_Abilities,Name,Title,Description,SelfInsight")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Name", publisher.GameId);
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id && m.GameId==GameId());
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var publisher = await _context.Publisher
                .Include(p=>p.BaseProperties)
                .FirstOrDefaultAsync(p=>p.Id==id && p.GameId==GameId());
            var properties = _context.Properties.Where(p => p.PresetId == publisher.Id);
            _context.Properties.RemoveRange(properties);
            _context.BaseProperties.RemoveRange(publisher.BaseProperties);
            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(long id)
        {
            return _context.Publisher.Any(e => e.Id == id);
        }
    }
}
