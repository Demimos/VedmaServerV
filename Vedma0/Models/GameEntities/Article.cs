using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.GameEntities
{
    public class Article : GameEntity
    {
        /// <summary>
        /// No root, m_*** - image medium
        /// s_*** - image small
        /// </summary>
        public string Image { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
      
        public long? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
