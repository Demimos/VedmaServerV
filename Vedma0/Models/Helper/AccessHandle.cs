using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Data;

namespace Vedma0.Models.Helper
{
    public class AccessHandle
    {
        public static bool GameAccessCheck(HttpContext context, VedmaUser user, Game game)
        {
            if (!game.GameUsers.Select(gu=>gu.VedmaUserId).Contains(user.Id)&&game.OwnerId!=user.Id)
            {
                if (context.Request.Cookies.ContainsKey("in_Game"))
                {
                    context.Response.Cookies.Delete("in_Game");
                }
                return false;
            }
            return true;
        }
        public static bool GameMasterCheck(HttpContext context, VedmaUser user, Game game)
        {
            return GameAccessCheck(context, user, game) && IsMaster(user, game);
        }
        public static bool IsMaster(VedmaUser user, Game game)
        {
            return game.OwnerId == user.Id || game.MasterIds.Contains(user.Id);
        }
    }
}
