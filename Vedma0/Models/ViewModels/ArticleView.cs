using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.GameEntities;

namespace Vedma0.Models.ViewModels
{
    public class ArticleView
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        public IList<string> Images { get; set;}
        public long PublisherId { get; set; }

        public ArticleView(Article article)
        {
            this.Id = article.Id;
            this.Title = article.Name;
            this.Image = article.Image;
            this.DateTime = article.DateTime;
            this.Body = article.Body;
            this.Images = article.Images;
            this.PublisherId = (long)article.PublisherId;
        }
    }
}
