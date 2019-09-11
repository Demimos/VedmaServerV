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
            character.Properties = await _context.Properties
                .AsNoTracking()
                .Include(p=>p.Preset)
                .Where(p => p.GameEntityId == character.Id &&  p.Visible)
                .ToListAsync();
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
        public async Task<IActionResult> Contacts()
        {
            var uid = UserId();
            var gid = GameId();
            var character = await _context.Characters
                .Include(p=>p.Contacts)
                .ThenInclude(p=>p.Reflection)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == uid);
            return View(character.Contacts.Select(p=>new ContactView(p.Reflection)));
        }

        public async Task<IActionResult> Contact(long id)
        {
            var uid = UserId();
            var gid = GameId();
            var character = await _context.Characters
                .Include(p => p.Contacts)
                .ThenInclude(p => p.Reflection)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == uid);
            var contact = character.Contacts.FirstOrDefault(p => p.ReflectionId == id).Reflection;
            if (contact == null)
                return NotFound();
            return View(new ContactView(contact));
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


        // GET: InGame/Edit/5
        public async Task<IActionResult> News()
        {
            var uid = UserId();
            var character = await _context.Characters
                .Include(p=>p.Properties)
                .ThenInclude(p=>p.Preset)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == uid);

            var publishers = character.Properties.Select(p => p.Preset).Where(p => p is Publisher).Select(p=>new PublisherView((Publisher)p));
            return View(publishers);
        }

        public async Task<IActionResult> News(long publisherId)
        {
            var uid = UserId();
            var gid = GameId();
            var character = await _context.Characters
                .Include(p => p.Properties)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == uid);

            if (!character.Properties.Select(p => (long)p.PresetId).ToList().Contains(publisherId))
                return NotFound();

            var news = await _context.Articles
                .AsNoTracking()
                .Where(p => p.PublisherId == publisherId)
                .Select(p=>new ArticleView(p))
                .ToListAsync();

            return View(news);
        }

        public async Task<IActionResult> NewsOne(long articleId)
        {
            var uid = UserId();
            var gid = GameId();
            var character = await _context.Characters
                .Include(p => p.Properties)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == uid);

            var article = await _context.Articles
                .Include(p=>p.Publisher)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == articleId);

            if (article==null || !character.Properties
                .Select(p => (long)p.PresetId)
                .ToList()
                .Contains((long)article.PublisherId))
                return NotFound();

            return View(new ArticleView(article));
        }


        // GET: InGame/Quit
        public ActionResult Quit()
        {
            HttpContext.Response.Cookies.Delete("in_Game");
            return RedirectToAction("Index","Home",null);
        }
    }
}