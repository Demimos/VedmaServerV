using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    public class BaseTextProperty : BaseProperty
    {
        [Required]
        [DisplayName("Значение по умолчанию")]
        public string DefaultValue { get; set; }

        public override string GetValue()
        {
            return DefaultValue;
        }
    }
}
