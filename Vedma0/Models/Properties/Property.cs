using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    public abstract class Property : IProperty
    {

        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        public abstract string GetName();

        public abstract string GetValue();
    }
}
