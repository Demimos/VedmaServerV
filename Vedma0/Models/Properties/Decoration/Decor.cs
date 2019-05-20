using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties.Decoration
{
    public class Decor
    {
        public int? ForeColor { get; set; }
        public int? TextColor { get; set; }
        public int TextSize { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool CrossOut { get; set; }

    }
}
