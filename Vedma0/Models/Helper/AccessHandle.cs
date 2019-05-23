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
        public static bool IsMaster(string userId, Game game)
        {
            return game.OwnerId == userId || game.MasterIds.Contains(userId);
        }
    }
}
