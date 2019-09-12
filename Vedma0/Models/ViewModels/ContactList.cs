using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.ViewModels
{
    public class ContactList
    {
        public IList<ContactView> Contacts { get; set; }
        public IList<ContactView> Watchers { get; set; }

        public ContactList(Character character)
        {
            this.Contacts = character.Contacts
                .Select(p=>p.Reflection)
                .Select(p=> new ContactView(p))
                .ToList();
            this.Watchers = character.Watchers
                .Select(p => p.Owner)
                .Select(p => new ContactView(p))
                .ToList();
        }
    }
}
