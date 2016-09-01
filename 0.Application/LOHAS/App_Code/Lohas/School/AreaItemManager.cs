using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lohas.Area
{
    public class AreaItemManager
    {
        private static List<AreaItem> _AreaItemCache;

        public static void Initial()
        {
            _AreaItemCache = AreaItemAccessor.SelectAll()
                                .OrderByDescending(b => b.Sort)
                                .ToList();
        }

        public static int GetSort()
        {
            int Sort = 0;

            if (_AreaItemCache.Count() != 0)
            { Sort = _AreaItemCache.Max(s => s.Sort); }

            return Sort + 1;
        }

        public static List<AreaItem> GetAll()
        {
            return _AreaItemCache.ToList(); 
        }

        public static AreaItem GetById(string Id)
        {
            return _AreaItemCache.FirstOrDefault(c => c.Id == Id);
        }

        public static AreaItem GetByName(string Name)
        {
            return _AreaItemCache.FirstOrDefault(c => c.Name == Name);
        }


        static object SaveObject = new object();
        public static void Save(AreaItem AreaItem)
        {
            if (AreaItem.Id == "-1") { AreaItem.Id = Guid.NewGuid().ToString(); }

            AreaItem.UpdateTime = DateTime.Now;

            lock (SaveObject)
            {
                //更新資料庫
                AreaItemAccessor.UpdateInsert(AreaItem);

                //記憶體重新初始化
                Initial();
            }
        }

        public static void Remove(AreaItem AreaItem)
        {
            //更新資料庫
            AreaItemAccessor.Delete(AreaItem);

            //更新記憶体
            _AreaItemCache.Remove(AreaItem);

        }


    }
}