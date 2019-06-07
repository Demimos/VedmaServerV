
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;
using Vedma0.Models.Logging;
using Vedma0.Models.ManyToMany;
using Vedma0.Models.Properties;

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
        [Required]
        public string _MasterIds { get; set; }
        [Required]
        public string _BlackList { get; set; }

        public IList<GameEntity> GameEntities { get; set; }
        public IList<Preset> Presets { get; set; }
        public IList<BaseProperty> BaseProperties { get; set; }
        public IList<EntityProperty> EntityProperties { get; set; }
        //public IList<Log> Logs { get; set; }
        public IList<GameUser> GameUsers { get; set; }

        [NotMapped]
        public IList<string> BlackList
        {
            get => JsonConvert.DeserializeObject<IList<string>>(_BlackList);
            set => _BlackList = JsonConvert.SerializeObject(value);
        }

        [NotMapped]
        public IList<string> MasterIds
        {
            get => JsonConvert.DeserializeObject<IList<string>>(_MasterIds);
            set => _MasterIds = JsonConvert.SerializeObject(value);
        }


        public Game()
        {
            MasterIds = new List<string>();
            BlackList = new List<string>();
            GameEntities = new List<GameEntity>();
            Presets = new List<Preset>();
            BaseProperties = new List<BaseProperty>();
            EntityProperties = new List<EntityProperty>();
            GameUsers = new List<GameUser>();
        }
    }
}
