using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    public abstract class BaseProperty :IProperty
    {
        public BaseProperty()
        {
            Properties = new List<EntityProperty>();
        }
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
        [DisplayName("Видимое")]
        public bool Visible { get; set; }

        [Required]
        [DisplayName("Видимое другим людям")]
        public bool Reflected { get; set; }


        public IList<EntityProperty> Properties { get; set; }

        public string GetName
        {
            get=> Name;
        }

        public abstract string GetPropertyType { get; }

        public abstract string GetValue { get; }
    }
    public enum PropertyType
    {
        Text,
        Number,
        TextArray,
        Identity
    }
}
