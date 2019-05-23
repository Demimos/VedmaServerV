using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vedma0.Data;

namespace Vedma0.Models.Helper
{

    [AttributeUsage(AttributeTargets.Class)]
    public class AccessRule : Attribute, IActionFilter
    {
        private readonly AccessLevel _accessLevel;
        public ApplicationDbContext Db { get; private set; }
        public Game Game { get; set; }
        
        public AccessRule(AccessLevel accessLevel=AccessLevel.Player)
        {
            _accessLevel = accessLevel;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                HandleNoAuth(context);
            }
            if (!context.HttpContext.Request.Cookies.ContainsKey("in_Game"))
            {
                HandleNoCookies(context);
            }
            var Id = context.HttpContext.Request.Cookies["in_Game"];
            if (!Guid.TryParse(Id, out Guid gid))
            {
                HandleWrongNumber(context);
            }
            Db = (ApplicationDbContext)context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext));
            Game = Db.Games.Include(g=>g.GameUsers).FirstOrDefault(g => g.Id == gid);
            var uid = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            switch (_accessLevel)
            {
                case AccessLevel.Player:
                    if (!Game.GameUsers.Select(g => g.VedmaUserId).Contains(uid))
                        HandleWrongNumber(context);
                        break;
                case AccessLevel.Developer:
                    if (!(Game.MasterIds.Contains(uid)||Game.OwnerId==uid))
                        HandleWrongNumber(context);
                    break;
            }
               
        }

        private void HandleNoAuth(ActionExecutingContext context)
        {
            throw new NotImplementedException();//TODO HandleNoAuth
        }

        private void HandleWrongNumber(ActionExecutingContext context)
        {
            switch (_accessLevel)
            {
                case AccessLevel.Player:
                    throw new NotImplementedException();//TODO HandleNoAuth
                case AccessLevel.Developer:
                    throw new NotImplementedException();//TODO HandleNoAuth
            }
        
        }

        private void HandleNoCookies(ActionExecutingContext context)
        {
            throw new NotImplementedException();//TODO HandleNoCookies
        }
    }
    public enum AccessLevel
    {
        Player,
        Developer
    }
}
