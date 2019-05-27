using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;
using Vedma0.Models.ManyToMany;
using Vedma0.Models.Properties;

namespace Vedma0.Models
{
    public class Preset
    {
        public Preset()
        {
            EntityPresets = new List<EntityPreset>();
            BaseProperties = new List<BaseProperty>();
            EntityProperties = new List<EntityProperty>();
            _Abilities = "[]";
        }
        public long Id { get; set; }
        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
      
        public long SortValue { get; set; }
        
        public string _Abilities { get; set; }

        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required]
        public bool SelfInsight { get; set; }

        public IList<EntityPreset> EntityPresets { get; set; }
        public IList<BaseProperty> BaseProperties { get; set; }
        public IList<EntityProperty> EntityProperties { get; set; }

    }
}
