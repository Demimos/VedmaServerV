using System.Collections;
using System.Collections.Generic;

namespace Vedma0.Models.GameEntities
{
    public class Publisher:Preset
    {
        Publisher(): base()
        {
            Articles = new List<Article>();
        }
        public string Adress { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// No root, m_*** - image medium
        /// s_*** - image small
        /// </summary>
        public byte[] Image { get; set; }
        public bool AllowAnonymus { get; set; }
        public IEnumerable<Article> Articles { get; set; }

    }
}