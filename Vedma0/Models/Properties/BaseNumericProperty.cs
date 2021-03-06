﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    public class BaseNumericProperty:BaseProperty
    {
        [Required]
        [DisplayName("Значение по умолчанию")]
        public double DefaultValue { get; set; }

        public override string GetPropertyType
        {
           get=> "Числовое свойство";
        }

        public override string GetValue
        {
            get => string.Format(CultureInfo.InvariantCulture,"{0:0.##}", DefaultValue);
        }
    }
}
