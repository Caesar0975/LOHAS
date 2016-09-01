using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.School
{
    public class SchoolItem
    {
        public string AreaItemId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public SchoolItem(string AreaItemId, string Nmae)
        {
            this.AreaItemId = AreaItemId;
            this.Id = "-1";
            this.Name = Name;
            this.Sort = SchoolItemManager.GetSort();
            this.UpdateTime  = DateTime.Now;
            this.CreateTime  = DateTime.Now;
        }

        internal SchoolItem(string AreaItemId, string Id, string Name, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.AreaItemId = AreaItemId;
            this.Id = Id;
            this.Name = Name;
            this.Sort = Sort;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}
