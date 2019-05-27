using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.Properties
{
    public class NumericProperty : EntityProperty
    {
        [Required]
        [DisplayName("Значение")]
        public double Value { get; set; }

        public override string GetValue
        {
            get=> Value.ToString("F", CultureInfo.InvariantCulture);
        }

        public override string GetPropertyType
        {
            get => "Числовое свойство";
        }

        public NumericProperty() : base()
        {

        }
        public NumericProperty(BaseNumericProperty bp, GameEntity ge) : base()
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
    }
}
