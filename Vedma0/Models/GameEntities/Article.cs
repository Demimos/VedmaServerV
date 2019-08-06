using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.GameEntities
{
    public class Article : GameEntity
    {

        public Article() : base()
        {
            Images = new List<string>();
        }

        /// <summary>
        /// No root, m_*** - image medium
        /// s_*** - image small
        /// </summary>
        public string Image { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        
        public string _Images { get; set; }

        [NotMapped]
        public IList<string> Images
        {
            get => JsonConvert.DeserializeObject<IList<string>>(_Images);
            set => _Images = JsonConvert.SerializeObject(value);
        }

        public long? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
