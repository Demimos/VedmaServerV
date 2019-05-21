using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.ManyToMany
{
    public class EntityPreset
    {
        public long GameEntityId { get; set; }
        public GameEntity GameEntity { get; set; }

        public long PresetId { get; set; }
        public Preset Preset { get; set; }
    }
}
