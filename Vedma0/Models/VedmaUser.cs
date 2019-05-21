using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;
using Vedma0.Models.ManyToMany;

namespace Vedma0.Models
{
    public class VedmaUser:IdentityUser
    {
        public string PushToken { get; set; }
        public Guid? CurrentGame { get; set; }
        public bool EmailSignal { get; set; }

        public IList<GameUser> GameUsers { get; set; }
        public IList<Character> Characters { get; set; }
        public VedmaUser() : base()
        {

            GameUsers = new List<GameUser>();
     
            Characters = new List<Character>();
        }
    }
}
