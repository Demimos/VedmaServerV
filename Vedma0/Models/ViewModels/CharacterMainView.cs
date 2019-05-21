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
            Pages= character.Properties.Select(pr=>pr.Preset).GroupBy(p => p.Title).Select(group=>new PropertyPageView(group)).OrderBy(pv=>pv.SortValue).ToList();
        }
    }
}
