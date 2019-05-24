using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.Logging;

namespace Vedma0.Models.GameEntities
{
    public class Character:GameEntity
    {
        public string UserId { get; set; }
        [DisplayName("Пользователь")]
        public VedmaUser User { get; set; }
        [Required]
        [DisplayName("Активен")]
        public bool Active { get; set; } = false;
        [Required]
        [DisplayName("Неполученные сообщения")]
        public bool HasSuspendedSignal { get; set; } = false;
        [DisplayName("Сообщение при неактивности")]
        public string InActiveMessage { get; set; } = "Ваш персонаж ещё не закончен";
       

        public IList<DiaryPage> Diary { get; set; }
        


        public Character()
        {
            Diary = new List<DiaryPage>();

        }
    }
}
