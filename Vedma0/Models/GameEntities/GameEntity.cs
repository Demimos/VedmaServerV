using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.GameEntities
{
    public abstract class GameEntity
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
     
    }
}
