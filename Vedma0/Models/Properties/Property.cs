using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.Properties
{
    public abstract class Property : BaseProperty
    {

        [Required]
        public long GameEntityId { get; set; }
        public GameEntity GameEntity { get; set; }
        [Required]
        public long BasePropertyId { get; set; }
        public BaseProperty BaseProperty { get; set; }

    }
}
