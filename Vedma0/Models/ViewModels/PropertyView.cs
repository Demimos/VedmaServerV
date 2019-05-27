using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.Properties;

namespace Vedma0.Models.ViewModels
{
    public class PropertyView
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public PropertyView(EntityProperty p)
        {
            Title = p.GetName;
            Text = p.GetValue;
        }
    }
}
