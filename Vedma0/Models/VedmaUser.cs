using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models
{
    public class VedmaUser:IdentityUser
    {
        public IList<Game> Games { get; set; }
        public VedmaUser() : base()
        {
            Games = new List<Game>();
        }
    }
}
