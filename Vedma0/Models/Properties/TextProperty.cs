using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.Properties
{
    public class TextProperty : EntityProperty
    {
        [Required]
        [DisplayName("Значение")]
        public string Value { get; set; }
        public override string GetValue()
        {
            return Value;
        }

        public TextProperty():base()
        {

        }
        public TextProperty(BaseTextProperty bp, GameEntity ge) : base()
        {
            Name = bp.Name;
            BasePropertyId = bp.Id;
            Description = bp.Description;
            GameId = bp.GameId;
            GameEntityId = ge.Id;
            Visible = bp.Visible;
            if (ge.GameId != bp.GameId)
                throw new FormatException("Game data Leak");
            Value = bp.DefaultValue;
            PresetId = bp.PresetId;
            SortValue = bp.SortValue;
        }
        public override string GetPropertyType()
        {
            return "Текстовое свойство";
        }
    }
}
