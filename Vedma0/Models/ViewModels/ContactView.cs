using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.ViewModels
{
    public class ContactView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<PropertyView> Properties { get; set; }

        public ContactView(Character character)
        {
            this.Id = character.Id;
            this.Name = character.Name;
            this.Properties = character.Properties.Where(p=>p.BaseProperty.Visible).Select(p => new PropertyView(p)).ToList();
        }
    }
}
