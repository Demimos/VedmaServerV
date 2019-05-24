using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vedma0.Controllers;
using Vedma0.Data;

namespace Vedma0.Models.Helper
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public sealed class AccessRule : Attribute, IActionFilter
    {
        private readonly AccessLevel _accessLevel;
        public ApplicationDbContext Db { get; private set; }
        public Game CurrentGame { get; private set; }
        public string Uid { get; private set; }
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
                return;
            }
            if (!context.HttpContext.Request.Cookies.ContainsKey("in_Game"))
            {
                RedirectHome(context); 
            }
            var Id = context.HttpContext.Request.Cookies["in_Game"];
            if (!Guid.TryParse(Id, out Guid gid))
            {
                HandleWrongNumber(context);
            }
            Db = (ApplicationDbContext)context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext));
            CurrentGame = Db.Games.AsNoTracking().Include(g=>g.GameUsers).FirstOrDefault(g => g.Id == gid);
            Uid = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            switch (_accessLevel)
            {
                case AccessLevel.Player:
                    if (!CurrentGame.GameUsers.Select(g => g.VedmaUserId).Contains(Uid) && CurrentGame.OwnerId != Uid)
                        HandleWrongNumber(context);
                        break;
                case AccessLevel.Developer:
                    if (!CurrentGame.MasterIds.Contains(Uid) && CurrentGame.OwnerId != Uid)
                        RedirectHome(context);  //TODO customize
                    break;
            }

        }
       

        private void HandleNoAuth(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new {  controller = "Identity/Account", action = "Login" })
            );
            context.Result.ExecuteResultAsync(controller.ControllerContext);
        }

        private void HandleWrongNumber(ActionExecutingContext context)
        {
            context.HttpContext.Response.Cookies.Delete("in_game");
            RedirectHome(context);           
        }

        private void RedirectHome(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            context.Result = new RedirectToRouteResult(
                      new RouteValueDictionary(new { controller = "Games", action = "Index" }) 
                  );
            context.Result.ExecuteResultAsync(controller.ControllerContext);
        }
    }
    public enum AccessLevel
    {
        Player,
        Developer
    }
}
