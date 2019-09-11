using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.ViewModels
{
    public class PublisherView
    {
        public long Id { get; set; }
        public long Sort { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// No root, m_*** - image medium
        /// s_*** - image small
        /// </summary>
        public string Image { get; set; }
        public long MessageQuota { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public readonly int ArticleCount;

        public PublisherView(Publisher publisher)
        {
            this.Id = publisher.Id;
            this.Sort = publisher.SortValue;
            this.Name = publisher.Title;
            this.Adress = publisher.Adress;
            this.Email = publisher.Email;
            this.Image = publisher.Image;
            this.Articles = publisher.Articles;
            this.ArticleCount = publisher.ArticleCount;
        }

    }
}
