using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.Logging
{
    public class DiaryPage :Log
    {
       
        [Required]
        public long CharacterId { get; set; }
        public Character Character { get; set; }
        public DiaryPageType Type { get; set; }
    }
    public enum DiaryPageType
    {
        Signal,
        User,
        Master
    }
}
