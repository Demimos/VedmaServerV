using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.ViewModels
{
    public class Selectable
    {
        public Selectable(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public Selectable() { }
        public  string Id { get; set; }
        public  string Name { get; set; }
    }
}
