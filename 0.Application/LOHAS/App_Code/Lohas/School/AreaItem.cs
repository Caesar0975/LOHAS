using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.Area
{
    public class AreaItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public AreaItem(string Name, int Sort)
        {
            this.Id          = "-1";
            this.Name        = Name;
            this.Sort        = AreaItemManager.GetSort();
            this.UpdateTime  = DateTime.Now;
            this.CreateTime  = DateTime.Now;
        }

        internal AreaItem(string Id, string Name, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id         = Id;
            this.Name       = Name;
            this.Sort       = Sort;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}