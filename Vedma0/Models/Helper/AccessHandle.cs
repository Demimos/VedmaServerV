using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Data;

namespace Vedma0.Models.Helper
{
    public class AccessHandle
    {
        public static bool GameAccessCheck(ApplicationDbContext context, VedmaUser user, Game game)
        {
            if (!game.GameUsers.Select(gu=>gu.VedmaUserId).Contains(user.Id)&&game.OwnerId!=user.Id)
            {
                user.CurrentGame = null;
                context.SaveChanges();
                return false;
            }
            return true;
        }
    }
}
