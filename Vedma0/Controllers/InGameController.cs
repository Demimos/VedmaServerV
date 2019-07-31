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
using Vedma0.Models.GameEntities;
using Vedma0.Models.Helper;
using Vedma0.Models.Logging;
using Vedma0.Models.Properties;
using Vedma0.Models.ViewModels;

namespace Vedma0.Controllers
{
    [AccessRule(AccessLevel.Player)]
    public class InGameController : VedmaController
    {
        private const int pagesPerSheet=30;
      
        public InGameController(ApplicationDbContext context):base(context)
        {  
        }
        // GET: InGame
        public async Task<ActionResult> Index(int? page)
        {
            var uid = UserId();
            var game = await GameAsync();
            var character = await GetCharacter(); 
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
            character.Properties = await _context.Properties.AsNoTracking().Include(p=>p.Preset).Where(p => p.GameEntityId == character.Id &&  p.Visible).ToListAsync();
            CharacterMainView main = new CharacterMainView(character);
            if (main.Pages.Count == 0)
                ViewBag.NoContent = true;
            else
            {
                ViewBag.NoContent = false;
                if (page != null && main.Pages.Count > (int)page)
                {
                    ViewBag.PageView = page;
                }
                else
                    ViewBag.PageView = 0;
                if (main.Pages.Count - 1 > ViewBag.PageView)
                    ViewBag.HasNext = true;
                else
                    ViewBag.HasNext = false;
                if (ViewBag.PageView > 0)
                {
                    ViewBag.HasPrevious = true;
                }
                else
                    ViewBag.HasPrevious = false;
            }
            return View(main);
        }

        //GET: InGame/Diary
        public async Task<IActionResult> Diary(int? page,  int? filter, bool? datesort)
        {
            if (page == null)
                page = 0;
            if (datesort == null)
                datesort = false;
            if (filter == null||filter>3)
                filter = 0;
            var game = await GameAsync();
            if (IsMaster(game))
            {
                ViewData["IsMaster"] = "true";
                ViewData["Id"] = game.Id;
            }
            ViewData["Title"] = game.Name;
            var character = await _context.Characters
              .AsNoTracking()
              .FirstOrDefaultAsync(d => d.GameId == (Guid)GameId() && d.UserId == UserId());
            if (character == null)
                return View("NoCharacter");
            IQueryable<DiaryPage> source = _context.Diary
                .AsNoTracking()
                .Where(d => d.CharacterId == character.Id);
            var diaryType = (DiaryFilter)filter;
            switch ((DiaryFilter)filter)
            {
                case DiaryFilter.All:  break;
                case DiaryFilter.Master: source = source.Where(p => p.Type == DiaryPageType.Master); break;
                case DiaryFilter.Player: source = source.Where(p => p.Type == DiaryPageType.User); break;
                case DiaryFilter.System: source = source.Where(p => p.Type == DiaryPageType.Signal); break;
            }
            var count = await source.CountAsync();
            IEnumerable<DiaryPage> items = await source.Skip((int)page * pagesPerSheet).Take(pagesPerSheet).ToListAsync();
            DiaryListViewModel pageViewModel = new DiaryListViewModel(items, count, pagesPerSheet , (int)page, (bool)datesort, diaryType);
            return View(pageViewModel);
        }

       
        //    if (diaryPage.Title == null || diaryPage.Message == null)
        //        return View();
        //    diaryPage.Type = DiaryPageType.User;
        //    diaryPage.GameId = (Guid) GameId();
        //    diaryPage.CharacterId = (await GetCharacter()).Id;
        //    diaryPage.DateTime = DateTime.UtcNow;
        //    try
        //    {
        //        _context.Diary.Add(diaryPage);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Diary));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

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

        // GET: InGame/Quit
        public ActionResult Quit()
        {
            HttpContext.Response.Cookies.Delete("in_Game");
            return RedirectToAction("Index","Home",null);
        }
    }
}