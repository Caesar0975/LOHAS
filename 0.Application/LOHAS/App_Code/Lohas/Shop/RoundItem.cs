using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.Round
{

    public class RoundItem
    {

        public string ShopItemId { get; set; }
        public string Id { get; set; }
        public string Name { get { return string.Format("{0} ~ {1}", this.StartTime.ToString(@"hh\:mm"), this.EndTime.ToString(@"hh\:mm")); } }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int LimitPairAmount { get; set; }


        public RoundItem(ShopItem ShopItem, TimeSpan StartTime, TimeSpan EndTime, int LimitPairAmount)
        {
            this.ShopItemId = ShopItem.Id;
            this.Id = Guid.NewGuid().ToString();
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.LimitPairAmount = LimitPairAmount;
        }

        internal RoundItem(string ShopItemId, string Id, TimeSpan StartTime, TimeSpan EndTime, int LimitPairAmount)
        {
            this.ShopItemId = ShopItemId;
            this.Id = Id;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.LimitPairAmount = LimitPairAmount;
        }
    }
}