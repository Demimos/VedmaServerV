
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.ManyToMany
{
    public class GameUser
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        public string VedmaUserId { get; set; }
        public VedmaUser VedmaUser { get;set; }
    }
}
