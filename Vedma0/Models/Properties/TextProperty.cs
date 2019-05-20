using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    public class TextProperty : Property
    {
        [Required]
        [DisplayName("Значение")]
        public string Value { get; set; }
        public override string GetValue()
        {
            return Value;
        }
    }
}
