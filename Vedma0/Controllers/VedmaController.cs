using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vedma0.Data;
using Vedma0.Models;
using Vedma0.Models.GameEntities;
using Vedma0.Models.Helper;

namespace Vedma0.Controllers
{
    public abstract class VedmaController:Controller
    {
        protected readonly ApplicationDbContext _context;
        public VedmaController(ApplicationDbContext context)
        {
            _context = context;
            if (context == null)
                throw new ArgumentNullException("ApplicationDbContext","База данных отсутствует в конструкторе контроллера");
        }
        protected async Task<Character> GetCharacter()
        {
            return await _context.Characters.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == UserId() && c.GameId == GameId());
        }
        /// <summary>
        /// Id текущей игры
        /// </summary>
        protected Guid? GameId()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("in_game"))
                return null;
            if (!Guid.TryParse(HttpContext.Request.Cookies["in_game"], out Guid gid))
            {
                Response.Cookies.Delete("in_game");
                return null;
            }
            return gid;
        }
        /// <summary>
        /// Id пользователя
        /// </summary>
        protected string UserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        /// <summary>
        /// Возвращает текущую игру NoTracking
        /// </summary>
        protected async Task<Game> GameAsync()
        {
            var gid = GameId();
            if (gid == null)
                return null;
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(g=>g.Id==(Guid)gid);
        }
        /// <summary>
        /// Проверка является ли пользователь мастером текущей игры
        /// </summary>
        /// <param name="context"></param>
        /// <param name="db"></param>
        public async Task<bool> IsMasterAsync()
        {
            var Gid = GameId();
            if (Gid == null)
                return false;
            var game = await _context.Games.AsNoTracking().FirstOrDefaultAsync(g => g.Id == Gid);
            if (game == null)
                return false;
            var userId = UserId();
            if (userId == null)
                return false;
            return game.OwnerId == userId || game.MasterIds.Contains(userId);
        }
        /// <summary>
        /// Проверка является ли пользователь мастером игры
        /// </summary>
        /// <param name="context"></param>
        /// <param name="db"></param>
        public bool IsMaster(Game game)
        {
            var Gid = GameId();
            if (Gid == null)
                return false;
            var userId = UserId();
            if (userId == null)
                return false;
            return game.OwnerId == userId || game.MasterIds.Contains(userId);
        }
       
    }
}
