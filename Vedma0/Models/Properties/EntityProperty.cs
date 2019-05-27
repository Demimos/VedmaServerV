using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.Properties
{
    public abstract class EntityProperty : IProperty
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        public long? PresetId { get; set; }
        public Preset Preset { get; set; }
        [Required]
        public long SortValue { get; set; }
        [Required]
        public bool Visible { get; set; }
        [Required]
        public long GameEntityId { get; set; }
        public GameEntity GameEntity { get; set; }
        [Required]
        public long BasePropertyId { get; set; }
        public BaseProperty BaseProperty { get; set; }

        public string GetName
        {
            get => Name;
        }

        public abstract string GetValue { get; }

        public abstract string GetPropertyType { get; }
    }
}
