using Lohas.Round;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.Shop
{
    public class ShopItem
    {
        public List<string> SchoolItemIds { get; set; }
        public string Id { get; internal set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<RoundItem> RoundItems { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public ShopItem(string Name, string Phone, string Address, string Latitude, string Longitude)
        {
            this.SchoolItemIds = new List<string>();
            this.Id = "-1";
            this.Name = Name;
            this.Phone = Phone;
            this.Address = Address;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.RoundItems = new List<RoundItem>();
            this.Sort = ShopItemManager.GetNewSort();
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal ShopItem(List<string> SchoolItemIds, string Id, string Name, string Phone, string Address, string Latitude, string Longitude, List<RoundItem> RoundItems, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.SchoolItemIds = SchoolItemIds;
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;
            this.Address = Address;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.RoundItems = RoundItems;
            this.Sort = Sort;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }
    }
}