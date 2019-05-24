using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.ManyToMany;
using Vedma0.Models.Properties;

namespace Vedma0.Models.GameEntities
{
    public abstract class GameEntity
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public string Tag { get; set; }
        public string QRTag { get; set; }

        public IList<EntityPreset> EntityPresets { get; set; }
        public IList<EntityProperty> Properties { get; set; }

        public GameEntity()
        {
            EntityPresets = new List<EntityPreset>();
            Properties = new List<EntityProperty>();
        }
    }
}
