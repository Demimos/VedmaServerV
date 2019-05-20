using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    public class NumericProperty : Property
    {
        [Required]
        [DisplayName("Значение")]
        public double Value { get; set; }

        public override string GetValue()
        {
            return Value.ToString("F", CultureInfo.InvariantCulture);
        }
    }
}
