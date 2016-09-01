using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeftHand.MemberShip;
using Lohas.Shop;
using Lohas.Round;
using Lohas.School;
using Lohas.Area;

namespace Lohas.Order
{
    public enum StateType { 新預約, 店長確認, 服務完成, 會員未到 }

    public class OrderItem
    {
        public decimal Id { get; set; }
        public string MemberAccount { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; }

        public string MemberArea { get; set; }
        public string MemberSchool { get; set; }

        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopPhone { get; set; }
        public string ShopAddress { get; set; }
        public string Shoplatitude { get; set; }
        public string Shoplongitude { get; set; }

        public DateTime? OrderDate { get; set; }
        public string OrderRound { get; set; }
        public StateType State { get; set; }

        public string QuestionResult { get; set; }

        public string SyatemRemark { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public OrderItem(ShopItem Shop, AreaItem AreaItem, SchoolItem SchoolItem, DateTime? OrderDate, string OrderRound)
        {
            this.Id = -1;
            this.MemberAccount = "";
            this.MemberName = "";
            this.MemberPhone = "";

            if (Shop != null)
            {
                this.ShopId = Shop.Id;
                this.ShopName = Shop.Name;
                this.ShopPhone = Shop.Phone;
                this.ShopAddress = Shop.Address;
                this.Shoplatitude = Shop.Latitude;
                this.Shoplongitude = Shop.Longitude;
            }

            if (AreaItem != null)
            { this.MemberArea = AreaItem.Name; }

            if (SchoolItem != null)
            { this.MemberSchool = SchoolItem.Name; }

            this.OrderDate = OrderDate;
            this.OrderRound = OrderRound;
            this.State = StateType.新預約;

            this.QuestionResult = "";

            this.SyatemRemark = "";

            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal OrderItem(decimal Id, string MemberAccount, string MemberName, string MemberPhone,
                         string MemberArea, string MemberSchool,
                         string ShopId, string ShopName, string ShopPhone, string ShopAddress, string Shoplatitude, string Shoplongitude,
                         DateTime OrderDate, string OrderRound, StateType State, string QuestionResult, string SyatemRemark, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.MemberAccount = MemberAccount;
            this.MemberName = MemberName;
            this.MemberPhone = MemberPhone;

            this.MemberArea = MemberArea;
            this.MemberSchool = MemberSchool;

            this.ShopId = ShopId;
            this.ShopName = ShopName;
            this.ShopPhone = ShopPhone;
            this.ShopAddress = ShopAddress;
            this.Shoplatitude = Shoplatitude;
            this.Shoplongitude = Shoplongitude;

            this.OrderDate = OrderDate;
            this.OrderRound = OrderRound;
            this.State = State;

            this.QuestionResult = QuestionResult;
            this.SyatemRemark = SyatemRemark;

            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}