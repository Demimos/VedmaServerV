using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.Properties;

namespace Vedma0.Models.ViewModels
{
    public class PropertyPageView
    {
        public string Title { get; set; }
        public IList<PropertyView> PropertyViews { get; set; }
        public int SortValue { get; set; }

        public PropertyPageView(Preset preset)
        {
            Title = preset.Title;
            PropertyViews = preset.EntityProperties.OrderBy(p => p.SortValue).Select(p => new PropertyView(p)).ToList();
        }
        public PropertyPageView(IGrouping<string, Preset> group)
        {
            PropertyViews = new List<PropertyView>();
            Title = group.Key;
            PropertyViews= group.SelectMany(p=>p.EntityProperties.OrderBy(pr => pr.SortValue)).Select(pr => new PropertyView(pr)).ToList();
            SortValue = group.Select(p => p.SortValue).Min();
        }
    }
}
