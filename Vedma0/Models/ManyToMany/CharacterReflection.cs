using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.ManyToMany
{
    public class CharacterReflection
    {
        public long OwnerId { get; set; }
        public Character Owner { get; set; }

        public long? ReflectionId { get; set; }
        public Character Reflection { get; set; }
    }
}
