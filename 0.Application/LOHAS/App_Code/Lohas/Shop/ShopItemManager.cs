using Lohas.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.Shop
{
    public class ShopItemManager
    {
        private static List<ShopItem> ShopItemCache;

        public static void Initial()
        {
            ShopItemCache = ShopItemAccessor.SelectAll()
                                .OrderBy(b => b.Sort)
                                .ThenByDescending(c => c.CreateTime)
                                .ToList();
        }

        public static int GetNewSort()
        {
            return (ShopItemCache.Count == 0) ? 1 : ShopItemCache.Max(s => s.Sort) + 1;
        }

        public static List<ShopItem> GetAll()
        {
            return ShopItemCache.ToList();
        }

        public static ShopItem GetById(string Id)
        {
            return ShopItemCache.FirstOrDefault(s => s.Id == Id);
        }

        public static ShopItem GetByName(string Name)
        {
            return ShopItemCache.FirstOrDefault(s => s.Name == Name);
        }

        public static List<ShopItem> GetBySchool(SchoolItem School)
        {
            if (School == null) { return new List<ShopItem>(); }

            return ShopItemCache.Where(s => s.SchoolItemIds.Contains(School.Id)).ToList();
        }

        static object SaveObject = new object();
        public static void Save(ShopItem ShopItem)
        {
            if (ShopItem.Id == "-1") { ShopItem.Id = Guid.NewGuid().ToString(); }

            ShopItem.UpdateTime = DateTime.Now;
            ShopItem.RoundItems = ShopItem.RoundItems.OrderBy(r => r.StartTime).ToList();

            lock (SaveObject)
            {
                //更新資料庫
                ShopItemAccessor.UpdateInsert(ShopItem);

                //更新記憶體
                ShopItemCache.Remove(ShopItem);
                ShopItemCache.Add(ShopItem);
            }
        }

        public static void Remove(ShopItem ShopItem)
        {
            //更新資料庫
            ShopItemAccessor.Delete(ShopItem);

            //更新記憶体
            ShopItemCache.Remove(ShopItem);

        }
    }
}
