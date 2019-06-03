using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Data;
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

        public void AddPreset(GameEntity gameEntity, ApplicationDbContext db)
        {
            if (!gameEntity.EntityPresets.Select(ep=>ep.PresetId).Contains(Id))
            {
                gameEntity.EntityPresets.Add(new EntityPreset { PresetId = Id, GameEntityId = gameEntity.Id });
                foreach (BaseTextProperty baseProperty in BaseProperties.Where(bp => bp is BaseTextProperty))
                {
                    var Property = new TextProperty(baseProperty, gameEntity);
                    db.Properties.Add(Property);
                }
                foreach (BaseTextArrayProperty baseProperty in BaseProperties.Where(bp => bp is BaseTextArrayProperty))
                {
                    var Property = new TextArrayProperty(baseProperty, gameEntity);
                    db.Properties.Add(Property);
                }
                foreach (BaseNumericProperty baseProperty in BaseProperties.Where(bp => bp is BaseNumericProperty))
                {
                    var Property = new NumericProperty(baseProperty, gameEntity);
                    db.Properties.Add(Property);
                }
            }
        }
        public void RemovePreset(GameEntity gameEntity, ApplicationDbContext db)
        {
            var entityPreset = gameEntity.EntityPresets.FirstOrDefault(ep => ep.PresetId == Id);
            if (entityPreset!=null)
            {
                gameEntity.EntityPresets.Remove(entityPreset);
                db.Properties.RemoveRange(gameEntity.Properties);
            }
        }
    }
}
