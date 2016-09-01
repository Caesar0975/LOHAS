using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.SlideShow
{
    public class SlideShowItem
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public bool Enable { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public SlideShowItem(string Image, bool Enable, string Url)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Image = Image;
            this.Enable = Enable;
            this.Url = Url;
            this.Sort = SlideShowItemManager.GetSort();
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal SlideShowItem(string Id, string Image, bool Enable, string Url, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.Image = Image;
            this.Enable = Enable;
            this.Url = Url;
            this.Sort =Sort;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }
    }

}
