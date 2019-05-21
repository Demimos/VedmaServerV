using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.Logging;

namespace Vedma0.Models.GameEntities
{
    public class Character:GameEntity
    {
        public string UserId { get; set; }
        public virtual VedmaUser User { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public bool HasSuspendedSignal { get; set; }
        public string InActiveMessage { get; set; }
        public IList<DiaryPage> Diary { get; set; }



        public Character()
        {
            Diary = new List<DiaryPage>();

        }
    }
}
