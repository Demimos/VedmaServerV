using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models
{
    public class Game
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        public string OwnerId { get; set; }

        [DisplayName("VR")]
        public bool IncludeVR { get; set; }
        [DisplayName("Геолокация")]
        public bool IncludeGeo { get; set; }
        [DisplayName("Пассивные геодействия")]
        public bool IncludeGeoFence { get; set; }
        [DisplayName("Новости")]
        public bool IncludeNews { get; set; }
        [DisplayName("Публикация новостей игроками")]
        public bool IncludeNewsPublishing { get; set; }
        [DisplayName("Рейтинг для новостей")]
        public bool IncludeNewsRate { get; set; }
        [DisplayName("Комментарии к новостям")]
        public bool IncludeNewsComments { get; set; }

        [DisplayName("Начало игры")]
        public DateTime StartTime { get; set; }
        [DisplayName("Конец игры")]
        public DateTime EndTime { get; set; }
        public bool Active { get; set; }

        public string MasterId { get; set; }
        public VedmaUser Master { get; set; }

        public Game()
        {
        }
    }
}
