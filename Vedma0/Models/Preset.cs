using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models
{
    public class Preset
    {
        public Preset()
        {
            GameEntities = new List<GameEntity>();
        }
        public long Id { get; set; }
        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public int SortValue { get; set; }
        
        public string _Abilities { get; set; }

        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
      
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required]
        public bool SelfInsight { get; set; }

        public IList<GameEntity> GameEntities { get; set; }

    }
}
