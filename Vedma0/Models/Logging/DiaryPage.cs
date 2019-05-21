using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.Logging
{
    public class DiaryPage 
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Текст")]
        public string Message { get; set; }
        [Required]
        [DisplayName("Дата и Время")]
        public DateTime DateTime { get; set; }

        [Required]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public long? CharacterId { get; set; }
        public Character Character { get; set; }
        public DiaryPageType Type { get; set; }
    }
    public enum DiaryPageType
    {
        Signal,
        User,
        Master
    }
}
