using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.ViewModels
{
    public class CharacterMainView
    {
        public long Id { get; set; }
        public IList<PropertyPageView> Pages { get; set; }
        public CharacterMainView()
        {
        }

        public CharacterMainView(Character character)
        {
            Id = character.Id;
            var presets = new List<Preset>();
            foreach(var preset in character.Properties.Select(pr => pr.Preset))
            {
                if (!presets.Contains(preset))
                    presets.Add(preset);
            }
            Pages= presets.GroupBy(p => p.Title).Select(group=>new PropertyPageView(group)).OrderBy(pv=>pv.SortValue).ToList();
        }
    }
}
